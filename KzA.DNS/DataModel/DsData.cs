using System;
using System.Buffers.Binary;
using System.Collections.Generic;
using System.Text;

namespace KzA.DNS.DataModel
{
    public class DsData : IRecordData
    {
        private ushort KeyTag;
        private DnsSecAlgorithm Algorithm;
        private DnsSecDsDigestAlgorithm DigestType;
        private byte[] Digest = [];

        public string ToZoneFile()
        {
            throw new NotImplementedException();
        }

        public static DsData Parse(ReadOnlySpan<byte> data, int offset, int length)
        {
            return new DsData
            {
                KeyTag = BinaryPrimitives.ReadUInt16BigEndian(data.Slice(offset, 2)),
                Algorithm = (DnsSecAlgorithm)data[offset + 2],
                DigestType = (DnsSecDsDigestAlgorithm)data[offset + 3],
                Digest = data.Slice(offset + 4, length - 4).ToArray()
            };
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"KeyTag: {KeyTag}")
              .AppendLine($"Algorithm: {Algorithm}")
              .AppendLine($"DigestType: {DigestType}")
              .Append($"Digest: {string.Join(' ', Digest.Select(b => b.ToString("X2")))}");
            return sb.ToString();
        }
    }
}
