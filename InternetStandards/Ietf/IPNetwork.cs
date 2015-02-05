using System;
using System.Collections;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Numerics;

namespace InternetStandards.Ietf
{
    public class IPNetwork
    {
        private readonly BitArray _prefixBits;
        private readonly IPAddress _ipAddress;
        
        internal IPNetwork(IPAddress networkNumber, byte prefixLength)
        {
            if (networkNumber.AddressFamily != AddressFamily.InterNetworkV6 && networkNumber.AddressFamily != AddressFamily.InterNetwork)
                throw new ArgumentOutOfRangeException("prefixLength");
            _ipAddress = networkNumber;


            if (prefixLength > networkNumber.GetAddressBytes().Length * 8)
                throw new ArgumentOutOfRangeException("prefixLength");
            PrefixLength = prefixLength;

            _prefixBits = new BitArray(new BitArray(_ipAddress.GetAddressBytes()).Cast<bool>().Take(PrefixLength).ToArray());
        }

        public static bool TryParse(string cidrNotation, out IPNetwork ipNetwork)
        {
            var parts = cidrNotation.Split(new[] { '/' }, 2);

            IPAddress ipAddress;
            byte prefixLength;

            if (parts.Length != 2 ||
                !IPAddress.TryParse(parts[0], out ipAddress) ||
                (ipAddress.AddressFamily != AddressFamily.InterNetwork && ipAddress.AddressFamily != AddressFamily.InterNetworkV6) ||
                !byte.TryParse(parts[1], out prefixLength) ||
                prefixLength > ipAddress.GetAddressBytes().Length * 8)
            {
                ipNetwork = null;
                return false;
            }

            ipNetwork = new IPNetwork(ipAddress, prefixLength);
            return true;
        }

        public static IPNetwork Parse(string cidrNotation)
        {
            IPNetwork ipNetwork;
            if(!TryParse(cidrNotation, out ipNetwork))
                throw new ArgumentException("cidrNotation");

            return ipNetwork;
        }

        public byte MaxPrefixLength
        {
            get { return (byte) (_ipAddress.GetAddressBytes().Length*8); }
        }

        public byte PrefixLength { get; private set; }

        public AddressFamily AddressFamily
        {
            get { return _ipAddress.AddressFamily; }
        }

        public bool Contains(IPAddress ipAddress)
        {
            return ipAddress.AddressFamily == AddressFamily &&
                   _prefixBits.Cast<bool>().SequenceEqual(
                       new BitArray(ipAddress.GetAddressBytes()).Cast<bool>().Take(PrefixLength));
        }

        public IPAddress StartAddress
        {
            get
            {
                var bytes = new byte[MaxPrefixLength/8];
                _prefixBits.CopyTo(bytes, 0);

                return new IPAddress(bytes);
            }
        }

        public IPAddress EndAddress
        {
            get
            {
                var bits = new BitArray(MaxPrefixLength, true);
                for(var i = 0; i < _prefixBits.Length; i++)
                    bits.Set(i, _prefixBits[i]);
                
                var bytes = new byte[MaxPrefixLength/8];
                bits.CopyTo(bytes, 0);

                return new IPAddress(bytes);
            }
        }

        public BigInteger AddressCount
        {
            get
            {
                return new BigInteger(2) ^  new BigInteger(MaxPrefixLength - PrefixLength);
            }
        }
    }
}
