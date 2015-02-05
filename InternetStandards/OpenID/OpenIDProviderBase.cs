using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Collections.Specialized;
using System.Security.Cryptography;
using System.Web.Caching;
using System.Numerics;
using System.Linq;

namespace InternetStandards.OpenID
{
    public abstract class OpenIDProviderBase
    {
        private HttpContextBase internalContext = null;

        public OpenIDProviderBase()
        {
            internalContext = new HttpContextWrapper(HttpContext.Current);
        }

        public OpenIDProviderBase(HttpContext context)
            : this(new HttpContextWrapper(context))
        {
        }

        public OpenIDProviderBase(HttpContextBase context)
        {
            internalContext = context;
        }

        public abstract bool Associate();

        protected void Associate(string associationHandle, int expiresIn)
        {
            HttpRequestBase request = internalContext.Request;
            HttpResponseBase response = internalContext.Response;
            Cache cache = internalContext.Cache;

            string openIDAssociationType = Validate(request.Form, "openid.assoc_type", new string[] { "HMAC-SHA1" }, new string[] { "HMAC-SHA256" }, null);
            string openIDSessionType = Validate(request.Form, "openid.session_type", new string[] { "DH-SHA1" }, new string[] { "DH-SHA256", "no-encryption" }, new string[] { string.Empty, null });

            KeyValueFormEncoding responseParameters = new KeyValueFormEncoding();

            responseParameters.Add("ns", "http://specs.openid.net/auth/2.0");
            responseParameters.Add("assoc_handle", associationHandle);
            responseParameters.Add("session_type", openIDSessionType);
            responseParameters.Add("assoc_type", openIDAssociationType);
            responseParameters.Add("expires_in", expiresIn.ToString());

            if (openIDSessionType == "no-encryption")
            {
                if (!request.IsSecureConnection)
                    throw new Exception();

                responseParameters.Add("enc_mac_key", Convert.ToBase64String(GenerateMacKey(128)));
            }
            else if (openIDSessionType == "DH-SHA1" || openIDSessionType == "DH-SHA256")
            {
                BigInteger p = new BigInteger(new byte[] { 220, 249, 58, 11, 136, 57, 114, 236, 14, 25, 152, 154, 197, 162, 206, 49, 14, 29, 55, 113, 126, 141, 149, 113, 187, 118, 35, 115, 24, 102, 230, 30, 247, 90, 46, 39, 137, 139, 5, 127, 152, 145, 194, 226, 122, 99, 156, 63, 41, 182, 8, 20, 88, 28, 211, 178, 202, 57, 134, 210, 104, 55, 5, 87, 125, 69, 194, 231, 229, 45, 200, 28, 122, 23, 24, 118, 229, 206, 167, 75, 20, 72, 191, 223, 175, 24, 130, 142, 253, 37, 25, 241, 78, 69, 227, 130, 102, 52, 175, 25, 73, 229, 181, 53, 204, 130, 154, 72, 59, 138, 118, 34, 62, 93, 73, 10, 37, 127, 5, 189, 255, 22, 242, 251, 34, 197, 131, 171 });
                BigInteger g = 2;

                string openIDDHModulus = request.Form["openid.dh_modulus"];
                if (!string.IsNullOrEmpty(openIDDHModulus))
                    try
                    {
                        p = new BigInteger(Convert.FromBase64String(openIDDHModulus));
                        //if (!p.IsProbablePrime())
                        //    return; // throw error
                    }
                    catch
                    {
                        //throw error
                    }

                string openIDDHGen = request.Form["openid.dh_gen"];
                if (!string.IsNullOrEmpty(openIDDHGen))
                    try
                    {
                        g = new BigInteger(Convert.FromBase64String(openIDDHGen));
                    }
                    catch
                    {
                        //throw error
                    }

                BigInteger dhConsumerPublic = new BigInteger(Convert.FromBase64String(request.Form["openid.dh_consumer_public"]));
                HashAlgorithm h = GetEncryptAssociationMethod(openIDSessionType);

                BigInteger xb = GenerateRandomPrivateKey(p - 1);

                responseParameters.Add("dh_server_public", Convert.ToBase64String(BTwoC(BigInteger.ModPow(g, xb, p).ToByteArray().Reverse().ToArray())));
                responseParameters.Add("enc_mac_key", Convert.ToBase64String(XOr(h.ComputeHash(BTwoC(BigInteger.ModPow(dhConsumerPublic, xb, p).ToByteArray().Reverse().ToArray())), GenerateMacKey(h.HashSize))));
            }
            else
            {
                // error
            }
        }

        public void Authenticate()
        {
            string openIDNamespace = GetIndirectRequestValue(internalContext.Request, "openid.ns");
            string openIDMode = GetIndirectRequestValue(internalContext.Request, "openid.mode");
            string openIDClaimedId = GetIndirectRequestValue(internalContext.Request, "openid.claimed_id");
            string openIDIdentity = GetIndirectRequestValue(internalContext.Request, "openid.identity");
            string openIDAssocateHandle = GetIndirectRequestValue(internalContext.Request, "openid.assoc_handle"); ;
            string openIDReturnTo = GetIndirectRequestValue(internalContext.Request, "openid.return_to"); ;
            string openIDRealm = GetIndirectRequestValue(internalContext.Request, "openid.realm"); ;

            KeyValueFormEncoding responseParameters = new KeyValueFormEncoding();

            responseParameters.Add("openid.ns", "http://specs.openid.net/auth/2.0");
            responseParameters.Add("openid.mode", "id_res");
            responseParameters.Add("openid.op_endpoint", (string)null);
            //responseParameters.Add("openid.claimed_id", openIDAssociationType);
            responseParameters.Add("openid.identity", (string)null);
            responseParameters.Add("openid.return_to", (string)null);
            responseParameters.Add("openid.response_nonce", GenerateNOuncePrefix() + GetNOnceUniqueSuffix());
            responseParameters.Add("openid.assoc_handle", (string)null);
            responseParameters.Add("openid.signed", string.Join(",", GetSignedFields(responseParameters)));
            responseParameters.Add("openid.sig ", (string)null);

        }

        public static string GetIndirectRequestValue(HttpRequestBase request, string key)
        {
            string contentType = request.ContentType;
            if(request.HttpMethod == "POST")
                return request.Form[key];

            return request.QueryString[key];
        }

        public void SendPositiveAssertion()
        {
        }

        public void SendNegativeAssertion()
        {
        }

        public void CheckAuthentication()
        {

        }

        public string GenerateSignature(HMAC hmac, KeyValueFormEncoding responseParameters)
        {
            KeyValueFormEncoding signedFields = new KeyValueFormEncoding();
            foreach (string field in GetSignedFields(responseParameters))
                if (responseParameters.ContainsKey("openid." + field))
                    signedFields.Add("openid." + field, responseParameters["openid." + field][0].ToString());

            return Convert.ToBase64String(hmac.ComputeHash(signedFields.ToBytes()));
        }

        protected abstract string GetNOnceUniqueSuffix();

        protected abstract byte[] GenerateMacKey(int size);

        protected abstract BigInteger GenerateRandomPrivateKey(BigInteger maxValue);

        public abstract HashAlgorithm GetEncryptAssociationMethod(string sessionType);

        public virtual string[] GetSignedFields(KeyValueFormEncoding responseParameters)
        {
            return new string[] { "op_endpoint", "return_to", "response_nonce", "assoc_handle", "claimed_id", "identity" };
        }

        public abstract int GetAssocationExpiration();

        protected string Validate(NameValueCollection parameters, string key, string[] validValues, string[] validOpenID20OnlyValues, string[] validOpenID11OnlyValues)
        {
            return Validate(parameters, key, validValues, validOpenID20OnlyValues, validOpenID11OnlyValues, true);
        }

        protected string Validate(NameValueCollection parameters, string key, string[] validValues, string[] validOpenID20OnlyValues, string[] validOpenID11OnlyValues, bool openID11CompatiblityMode)
        {
            if (validValues != null)
                foreach (string value in validValues)
                    if (parameters[key] == value)
                        return value;

            if (!openID11CompatiblityMode && validValues != null)
                foreach (string value in validOpenID20OnlyValues)
                    if (parameters[key] == value)
                        return value;

            if (openID11CompatiblityMode && validValues != null)
                foreach (string value in validOpenID11OnlyValues)
                    if (parameters[key] == value)
                        return value;

            // Error
            return null;
        }

        private static byte[] XOr(byte[] a, byte[] b)
        {
            long longestLength = Math.Max(a.LongLength, b.LongLength);
            byte[] xOrBytes = new byte[longestLength];
            for (long i = 0; i < longestLength; i++)
            {
                long aIndex = a.LongLength - longestLength + i;
                long bIndex = b.LongLength - longestLength + i;

                byte aByte = (aIndex < 0) ? (byte)0 : a[aIndex];
                byte bByte = (bIndex < 0) ? (byte)0 : b[bIndex];

                xOrBytes[i] = (byte)(aByte ^ bByte);
            }

            return xOrBytes;
        }

        private static byte[] BTwoC(byte[] bytes)
        {
            if (bytes[0] > 127 || bytes.Length > 1 && bytes[0] != 0)
            {
                byte[] appendedBytes = new byte[bytes.Length + 1];
                appendedBytes[0] = 0;
                bytes.CopyTo(appendedBytes, 1);

                bytes = appendedBytes;
            }

            return bytes;
        }

        private static string GenerateNOuncePrefix()
        {
            return DateTime.UtcNow.ToString("s") + "Z";
        }
    }
}
