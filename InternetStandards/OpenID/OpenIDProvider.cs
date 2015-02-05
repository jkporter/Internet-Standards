using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
using System.Runtime.InteropServices;
using System.Numerics;

namespace InternetStandards.OpenID
{
    public class OpenIDProvider:OpenIDProviderBase
    {
        protected override byte[] GenerateMacKey(int length)
        {
            RNGCryptoServiceProvider generator = new RNGCryptoServiceProvider();
            byte[] key = new byte[length];
            generator.GetBytes(key);

            return key;
        }

        public override HashAlgorithm GetEncryptAssociationMethod(string sessionType)
        {
            throw new NotImplementedException();
        }

        public override int GetAssocationExpiration()
        {
            throw new NotImplementedException();
        }

        protected override BigInteger GenerateRandomPrivateKey(BigInteger maxValue)
        {
            RNGCryptoServiceProvider r = new RNGCryptoServiceProvider();

            byte[] longValue = new byte[8];
            r.GetBytes(maxValue.ToByteArray());

            ByteArrayToLong(ref longValue);

            return BigInteger.Zero;
        }

        private static long ByteArrayToLong(ref byte[] bytes)
        {
            long result;

            GCHandle pinnedRawData = GCHandle.Alloc(bytes, GCHandleType.Pinned);
            try
            {
                IntPtr pinnedRawDataPtr = pinnedRawData.AddrOfPinnedObject();
                result = Marshal.ReadInt64(pinnedRawDataPtr);
            }
            finally
            {
                pinnedRawData.Free();
            }

            return result;
        }

        protected override string GetNOnceUniqueSuffix()
        {
            throw new NotImplementedException();
        }

        public override bool Associate()
        {
            throw new NotImplementedException();
        }
    }
}
