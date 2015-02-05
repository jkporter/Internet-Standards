using System;
using System.Web;
using System.Security.Cryptography;
using System.Numerics;
using System.Web.Caching;
using System.Linq;
using InternetStandards.Utilities.Collections.Specialized;

namespace InternetStandards.OpenID
{
    public class IISHandler1 : IHttpHandler
    {
        private const string openIDNS = "http://specs.openid.net/auth/2.0";
        private const int associationLifetime = 10;

        public delegate byte[] ComputeHash(byte[] buffer);
        /// <summary>
        /// You will need to configure this handler in the web.config file of your 
        /// web and register it with IIS before being able to use it. For more information
        /// see the following link: http://go.microsoft.com/?linkid=8101007
        /// </summary>
        #region IHttpHandler Members

        public bool IsReusable
        {
            // Return false in case your Managed Handler cannot be reused for another request.
            // Usually this would be false in case you have some state information preserved per request.
            get { return true; }
        }

        public void ProcessRequest(HttpContext context)
        {
            if (IsDirectRequest(context.Request))
            {
                switch (context.Request.Form["openid.mode"])
                {
                    case "associate":
                        ProcessAssociateSessionRequest(context);
                        break;
                    case "check_authentication":
                        break;
                }
            }
        }

        #endregion

        protected void ProcessAssociateSessionRequest(HttpContext context)
        {
            HttpRequest request = context.Request;
            HttpResponse response = context.Response;
            Cache cache = context.Cache;

            string associationType = request.Form["openid.assoc_type"];
            string sessionType = request.Form["openid.session_type"];
            string associationHandle = Guid.NewGuid().ToString();
            int expiresIn = GetAssociationLifetime();

            cache.Add(associationHandle, null, null, DateTime.Now.AddSeconds(expiresIn), Cache.NoSlidingExpiration, CacheItemPriority.NotRemovable, null);

            KeyValueFormEncoding responseParameters = new KeyValueFormEncoding();

            responseParameters.Add("ns", "http://specs.openid.net/auth/2.0");
            responseParameters.Add("assoc_handle", associationHandle);
            responseParameters.Add("session_type", sessionType);
            responseParameters.Add("assoc_type", associationType);
            responseParameters.Add("expires_in", expiresIn.ToString());

            if (sessionType == "no-encryption")
            {
                responseParameters.Add("enc_mac_key", Convert.ToBase64String(GenerateMacKey(128)));
            }
            else if (sessionType == "DH-SHA1" || sessionType == "DH-SHA256")
            {
                ComputeHash h = GetHashFunction(sessionType);

                BigInteger p = new BigInteger(new byte[] { 220, 249, 58, 11, 136, 57, 114, 236, 14, 25, 152, 154, 197, 162, 206, 49, 14, 29, 55, 113, 126, 141, 149, 113, 187, 118, 35, 115, 24, 102, 230, 30, 247, 90, 46, 39, 137, 139, 5, 127, 152, 145, 194, 226, 122, 99, 156, 63, 41, 182, 8, 20, 88, 28, 211, 178, 202, 57, 134, 210, 104, 55, 5, 87, 125, 69, 194, 231, 229, 45, 200, 28, 122, 23, 24, 118, 229, 206, 167, 75, 20, 72, 191, 223, 175, 24, 130, 142, 253, 37, 25, 241, 78, 69, 227, 130, 102, 52, 175, 25, 73, 229, 181, 53, 204, 130, 154, 72, 59, 138, 118, 34, 62, 93, 73, 10, 37, 127, 5, 189, 255, 22, 242, 251, 34, 197, 131, 171 });
                if (!string.IsNullOrEmpty(request.Form["openid.dh_modulus"]))
                    p = new BigInteger(Convert.FromBase64String(request.Form["openid.dh_modulus"]));

                BigInteger g = 2;
                if (request.Form["openid.dh_gen"] != null)
                    g = new BigInteger(Convert.FromBase64String(request.Form["openid.dh_gen"]));

                BigInteger dhConsumerPublic = new BigInteger(Convert.FromBase64String(request.Form["openid.dh_consumer_public"]));

                BigInteger xb = GenerateRandomPrivateKey(p - 1);

                int size = GetMacKeySize(sessionType);

                responseParameters.Add("dh_server_public", Convert.ToBase64String(BTwoC(BigInteger.ModPow(g, xb, p).ToByteArray().Reverse().ToArray())));
                responseParameters.Add("enc_mac_key", Convert.ToBase64String(XOr(h(BTwoC(BigInteger.ModPow(dhConsumerPublic, xb, p).ToByteArray().Reverse().ToArray())), GenerateMacKey(size))));
            }
        }

        protected void ProcessAuthenticateRequest(HttpContext context)
        {
            string mode = GetIndirectRequestParameter(context.Request, "openid.mode");
            string claimedID = GetIndirectRequestParameter(context.Request, "openid.claimed_id");
            string identity = GetIndirectRequestParameter(context.Request, "openid.identity");
            string associationHandle = GetIndirectRequestParameter(context.Request, "openid.assoc_handle");
            string returnTo = GetIndirectRequestParameter(context.Request, "openid.return_to");
            string realm = GetIndirectRequestParameter(context.Request, "openid.realm");

            if (returnTo != null)
            {
                string invalidateHandle = null;

                NamedValueList<string> fields = new NamedValueList<string>();

                fields.Add("openid.ns", openIDNS);
                fields.Add("openid.mode", "id_res");
                fields.Add("openid.op_endpoint", context.Request.Url.ToString());
                if (claimedID != null)
                    fields.Add("openid.claimed_id", claimedID);
                if (identity != null)
                    fields.Add("openid.identity", string.Empty);
                fields.Add("openid.return_to", returnTo);
                fields.Add("openid.response_nonce", GenerateNOunce(""));
                if (invalidateHandle != null)
                    fields.Add("openid.invalidate_handle", invalidateHandle);
                fields.Add("openid.assoc_handle", associationHandle);
                fields.Add("openid.signed", string.Join(",", fields.Keys.Skip(1).Select(k => k.Substring(7)).ToArray()));
                fields.Add("openid.sig", Convert.ToBase64String(CalculateSignature(fields)));

                // context.Response.Redirect(returnTo,
            }


            //KeyedHashAlgorithm signAlgorithm = GetAssocationType();
            //signAlgorithm.Key
        }



        protected void ProcessVerifyRequest(HttpContext context)
        {
            string mode = GetIndirectRequestParameter(context.Request, "openid.mode");
            string claimedID = GetIndirectRequestParameter(context.Request, "openid.claimed_id");
            string identity = GetIndirectRequestParameter(context.Request, "openid.identity");
            string associationHandle = GetIndirectRequestParameter(context.Request, "openid.assoc_handle");
            string returnTo = GetIndirectRequestParameter(context.Request, "openid.return_to");
            string realm = GetIndirectRequestParameter(context.Request, "openid.realm");

            // KeyedHashAlgorithm signAlgorithm = GetAssocationType();
            //signAlgorithm.Key
        }



        protected static byte[] CalculateSignature(NamedValueList<string> fields)
        {
            KeyValueFormEncoding keyValuePairs = new KeyValueFormEncoding();
            fields.Skip(1).TakeWhile(kv => kv.Name != "openid.signed")
                .ToList()
                .ForEach(f => keyValuePairs.Add(f.Name, f.Value));

            KeyedHashAlgorithm signAlgorithm = null;
            return signAlgorithm.ComputeHash(keyValuePairs.ToBytes());
        }

        public ComputeHash GetHashFunction(string sessionType)
        {
            switch (sessionType)
            {
                case "DH-SHA1":
                    return new SHA1Managed().ComputeHash;
                case "DH-SHA256":
                    return new SHA256Managed().ComputeHash;
            }

            return null;
        }

        public int GetMacKeySize(string sessionType)
        {
            switch (sessionType)
            {
                case "DH-SHA1":
                    return 160;
                case "DH-SHA256":
                    return 256;
            }

            return -1;
        }

        protected int GetAssociationLifetime()
        {
            return associationLifetime;
        }

        protected KeyedHashAlgorithm GetAssocationType(string associationType)
        {
            if (associationType == "HMAC-SHA256")
                return new HMACSHA256();

            if (associationType == "HMAC-SHA1")
                return new HMACSHA1();

            return null;
        }

        public void SendDirectResponse(HttpResponse response, KeyValueFormEncoding keyValueForm)
        {
            response.ContentType = "text/plain";
            response.Write(keyValueForm.ToString());
            response.End();
        }

        public byte[] GenerateMacKey(int length)
        {
            RNGCryptoServiceProvider generator = new RNGCryptoServiceProvider();
            byte[] key = new byte[length];
            generator.GetBytes(key);

            return key;
        }

        public BigInteger GenerateRandomPrivateKey(BigInteger maxValue)
        {
            RNGCryptoServiceProvider r = new RNGCryptoServiceProvider();

            //r.

            byte[] longValue = new byte[8];
            r.GetBytes(longValue);

            //ByteArrayToLong(ref longValue);

            return BigInteger.One;
        }

        public static byte[] XOr(byte[] a, byte[] b)
        {
            long longestLength = a.LongLength;
            if (b.LongLength > a.LongLength)
                longestLength = b.LongLength;

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

        public static byte[] BTwoC(byte[] bytes)
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

        public static bool IsDirectRequest(HttpRequest request)
        {
            return request.HttpMethod == "POST"
                && ValidHttpEncoding(request);
        }

        public static bool IsIndirectRequest(HttpRequest request)
        {
            return ValidHttpEncoding(request);
        }

        public static string GetIndirectRequestParameter(HttpRequest request, string name)
        {
            if (request.HttpMethod == "POST")
                return request.Form[name];

            return request.QueryString[name];
        }

        public static bool ValidHttpEncoding(HttpRequest request)
        {
            bool validContentType = (string.IsNullOrEmpty(request.ContentType) || request.ContentType == "application/x-www-form-urlencoded" || request.ContentType == "multipart/form-data");
            if (validContentType)
            {
                if (request.HttpMethod == "POST")
                {
                    bool foundOpenIDNS = false, foundOpenIDMode = false;
                    foreach (string name in request.Form.AllKeys)
                    {
                        if (!name.StartsWith("openid."))
                            return false;
                        else if (name == "openid.ns")
                            foundOpenIDNS = true;
                        else if (name == "openid.mode")
                            foundOpenIDMode = true;
                    }

                    return foundOpenIDNS && foundOpenIDMode;
                }

                return request.QueryString["openid.ns"] != null && request.QueryString["openid.mode"] != null;
            }

            return false;
        }

        public static string GenerateNOunce(string unique)
        {
            return DateTime.UtcNow.ToString("s") + "Z" + unique == null ? string.Empty : unique;
        }

        public class Association
        {
            public DateTime ExpiresAt
            {
                get;
                private set;
            }

            public string Handle
            {
                get;
                private set;
            }

            public string Type
            {
                get;
                set;
            }

            public string SessionType
            {
                get;
                set;
            }

            public KeyedHashAlgorithm TypeAlgorithm
            {
                get

                {
                    if (Type == "HMAC-SHA256")
                        return new HMACSHA256();

                    if (Type == "HMAC-SHA1")
                        return new HMACSHA1();

                    return null;
                }
            }

            public object SessionTypeParameters
            {
                get;
                private set;
            }
        }

        public class DiffeHellmanRequestParameters
        {
            public string Modules
            {
                get;
                private set;
            }
        }
     


        #region "Error Response"

        private void ErrorResponse(HttpResponse response, string error, string contact, string reference, KeyValueFormEncoding additionalFields)
        {
            response.ContentType = "text/plain";
            KeyValueFormEncoding reponseBody = new KeyValueFormEncoding();
            reponseBody.Add("ns", openIDNS);
            reponseBody.Add("error", error);
            if (contact != null) reponseBody.Add("contact", contact);
            if (reference != null) reponseBody.Add("reference", reference);

            response.Write(response);
            if (additionalFields != null) response.Write(additionalFields);

            response.End();
        }

        private void ErrorResponse(HttpResponse response, string error)
        {
            ErrorResponse(response, error, null, null, null);
        }

        private void ErrorResponse(HttpResponse response, string error, string contact)
        {
            ErrorResponse(response, error, contact, null, null);
        }

        private void ErrorResponse(HttpResponse response, string error, string contact, string reference)
        {
            ErrorResponse(response, error, contact, reference, null);
        }

        private void ErrorResponse(HttpResponse response, string error, KeyValueFormEncoding additionalFields)
        {
            ErrorResponse(response, error, null, null, additionalFields);
        }

        private void ErrorResponse(HttpResponse response, string error, string contact, KeyValueFormEncoding additionalFields)
        {
            ErrorResponse(response, error, contact, null, additionalFields);
        }

        #endregion
    }
}
