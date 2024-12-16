using KzA.DNS.Packet;
using System;
using System.Buffers.Binary;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KzA.DNS.DataModel
{
    public class SoaData : IRecordData
    {
        public HostData MNAME { get; set; } = new();
        public HostData RNAME { get; set; } = new();
        public uint SERIAL { get; set; }
        public uint REFRESH { get; set; }
        public uint RETRY { get; set; }
        public uint EXPIRE { get; set; }
        public uint MINIMUM { get; set; }

        public string ToZoneFile()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"{MNAME} {RNAME}(")
              .AppendLine($"                                         {SERIAL,-10};Serial")
              .AppendLine($"                                         {REFRESH,-10};Refresh")
              .AppendLine($"                                         {RETRY,-10};Retry")
              .AppendLine($"                                         {EXPIRE,-10};Expire")
              .AppendLine($"                                         {MINIMUM,-10};Minimum")
              .AppendLine("                                         )");
            return sb.ToString();
        }

        public static SoaData Parse(ReadOnlySpan<byte> data, int offset, ushort length)
        {
            SoaData soa = new();
            soa.MNAME.HostNameRaw = DomainName.Parse(data, ref offset);
            soa.RNAME.HostNameRaw = DomainName.Parse(data, ref offset);
            soa.SERIAL = BinaryPrimitives.ReadUInt32BigEndian(data.Slice(offset, 4));
            soa.REFRESH = BinaryPrimitives.ReadUInt32BigEndian(data.Slice(offset + 4, 4));
            soa.RETRY = BinaryPrimitives.ReadUInt32BigEndian(data.Slice(offset + 8, 4));
            soa.EXPIRE = BinaryPrimitives.ReadUInt32BigEndian(data.Slice(offset + 12, 4));
            soa.MINIMUM = BinaryPrimitives.ReadUInt32BigEndian(data.Slice(offset + 16, 4));

            return soa;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"Master: {MNAME}")
              .AppendLine($"Responsible: {RNAME}")
              .AppendLine($"Serial: {SERIAL}")
              .AppendLine($"Refresh: {REFRESH}")
              .AppendLine($"Retry: {RETRY}")
              .AppendLine($"Expire: {EXPIRE}")
              .Append($"Minimum: {MINIMUM}");
            return sb.ToString();
        }
    }
}
