using KzA.DNS.Packet;
using System;
using System.Buffers.Binary;
using System.Collections.Generic;
using System.Text;

namespace KzA.DNS.DataModel
{
    public class RRSigData : IRecordData
    {
        private RRType TypeCovered;
        private DnsSecAlgorithm Algorithm;
        private byte Labels;
        private uint OriginalTTL;
        private DateTime SignatureExpiration;
        private DateTime SignatureInception;
        private ushort KeyTag;
        private DomainName SignerName = new();
        private byte[] Signature = [];

        public string ToZoneFile()
        {
            throw new NotImplementedException();
        }

        public static RRSigData Parse(ReadOnlySpan<byte> data, int offset, int length)
        {
            int start = offset;
            var result = new RRSigData()
            {
                TypeCovered = (RRType)BinaryPrimitives.ReadUInt16BigEndian(data.Slice(offset, 2)),
                Algorithm = (DnsSecAlgorithm)data[offset + 2],
                Labels = data[offset + 3],
                OriginalTTL = BinaryPrimitives.ReadUInt32BigEndian(data.Slice(offset + 4, 4)),
                SignatureExpiration = DateTime.UnixEpoch.AddSeconds(BinaryPrimitives.ReadUInt32BigEndian(data.Slice(offset + 8, 4))).ToLocalTime(),
                SignatureInception = DateTime.UnixEpoch.AddSeconds(BinaryPrimitives.ReadUInt32BigEndian(data.Slice(offset + 12, 4))).ToLocalTime(),
                KeyTag = BinaryPrimitives.ReadUInt16BigEndian(data.Slice(offset + 16, 2)),
            };
            offset += 18;
            result.SignerName = DomainName.Parse(data, ref offset);
            result.Signature = data.Slice(offset, length - (offset - start)).ToArray();
            return result;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"TypeCovered: {TypeCovered}")
              .AppendLine($"Algorithm: {Algorithm}")
              .AppendLine($"Labels: {Labels}")
              .AppendLine($"OriginalTTL: {OriginalTTL}")
              .AppendLine($"SignatureExpiration: {SignatureExpiration:O}")
              .AppendLine($"SignatureInception: {SignatureInception:O}")
              .AppendLine($"KeyTag: {KeyTag}")
              .AppendLine($"SignerName: {SignerName}")
              .Append($"Signature: {string.Join(' ', Signature.Select(b => b.ToString("X2")))}");
            return sb.ToString();
        }
    }
}
