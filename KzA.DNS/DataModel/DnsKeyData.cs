using System;
using System.Buffers.Binary;
using System.Collections.Generic;
using System.Text;

namespace KzA.DNS.DataModel
{
    public class DnsKeyData : IRecordData
    {
        private DnsSecDnsKeyFlags Flags;
        private byte Protocol;
        private DnsSecAlgorithm Algorithm;
        private byte[] PublicKey = [];

        public string ToZoneFile()
        {
            throw new NotImplementedException();
        }

        public static DnsKeyData Parse(ReadOnlySpan<byte> data, int offset, int length)
        {
            return new DnsKeyData()
            {
                Flags = (DnsSecDnsKeyFlags)BinaryPrimitives.ReadUInt16BigEndian(data.Slice(offset, 2)),
                Protocol = data[offset + 2],
                Algorithm = (DnsSecAlgorithm)data[offset + 3],
                PublicKey = data.Slice(offset + 4, length - 4).ToArray()
            };
        }

        public override string ToString()
        {
            var isProtocolValid = Protocol == 3 ? string.Empty : " (Invalid)";
            var sb = new StringBuilder();
            sb.AppendLine($"Flags: {Flags}")
              .AppendLine($"Protocol: {Protocol}{isProtocolValid}")
              .AppendLine($"Algorithm: {Algorithm}")
              .Append($"PublicKey: {string.Join(' ', PublicKey.Select(b => b.ToString("X2")))}");
            return sb.ToString();
        }
    }
}
