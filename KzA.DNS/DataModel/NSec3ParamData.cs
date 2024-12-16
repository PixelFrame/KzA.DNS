using KzA.DNS.Packet;
using KzA.DNS.ResourceRecord;
using System;
using System.Buffers.Binary;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;

namespace KzA.DNS.DataModel
{
    public class NSec3ParamData : IRecordData
    {
        private DnsSecNSec3HashAlgorithm HashAlgorithm;
        private DnsSecNSec3ParamFlags Flags;
        private ushort Iterations;
        private byte SaltLength;
        private byte[] Salt = [];

        public string ToZoneFile()
        {
            throw new NotImplementedException();
        }

        public static NSec3ParamData Parse(ReadOnlySpan<byte> data, int offset, int length)
        {
            var result = new NSec3ParamData
            {
                HashAlgorithm = (DnsSecNSec3HashAlgorithm)data[offset],
                Flags = (DnsSecNSec3ParamFlags)data[offset + 1],
                Iterations = BinaryPrimitives.ReadUInt16BigEndian(data.Slice(offset + 2, 2)),
                SaltLength = data[offset + 4]
            };
            result.Salt = data.Slice(offset + 5, result.SaltLength).ToArray();
            return result;
        }

        public override string ToString()
        {

            var sb = new StringBuilder();
            sb.AppendLine($"HashAlgorithm: {HashAlgorithm}")
              .AppendLine($"Flags: {Flags}")
              .AppendLine($"Iterations: {Iterations}")
              .AppendLine($"Salt: ({SaltLength}) {string.Join(' ', Salt.Select(b => b.ToString("X2")))}");
            return sb.ToString();
        }
    }
}
