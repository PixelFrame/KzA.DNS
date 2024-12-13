using KzA.DNS.Packet;
using System;
using System.Buffers.Binary;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KzA.DNS.DataModel
{
    public class SvcbData : IRecordData
    {
        public ushort Priority { get; set; }
        public HostData Target { get; set; } = new("svc");
        public Dictionary<string, string> Params { get; set; } = [];

        public string ToZoneFile()
        {
            var sb = new StringBuilder();
            sb.Append(Priority).Append(' ').Append(Target).AppendLine(" (");
            foreach (var param in Params)
            {
                if (string.IsNullOrEmpty(param.Value))
                    sb.AppendLine(param.Key);
                else
                    sb.Append("                                         ").Append(param.Key).Append('=').AppendLine(param.Value);
            }
            sb.Append("                                         )");

            return sb.ToString();
        }

        public static SvcbData Parse(ReadOnlySpan<byte> data, int offset, ushort length)
        {
            var end = offset + length;
            SvcbData svcb = new()
            {
                Priority = BinaryPrimitives.ReadUInt16BigEndian(data.Slice(offset, 2)),
            };
            offset += 2;
            svcb.Target.HostNameRaw = DomainName.Parse(data, ref offset);
            while (offset < end)
            {
                var valueLen = BinaryPrimitives.ReadUInt16BigEndian(data.Slice(offset + 2, 2));
                var value = Encoding.UTF8.GetString(data.Slice(offset + 4, valueLen));
                svcb.Params.Add(((SvcbParamKeys)BinaryPrimitives.ReadUInt16BigEndian(data.Slice(offset, 2))).ToString(), value);
                offset += 4 + valueLen;
            }
            return svcb;
        }
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"Target  \t{Target}")
              .AppendLine($"Priority\t{Priority}")
              .AppendLine($"Params");
            foreach (var param in Params)
            {
                sb.AppendLine($"    {param.Key}:\t{param.Value}");
            }
            return sb.ToString();
        }
    }
}
