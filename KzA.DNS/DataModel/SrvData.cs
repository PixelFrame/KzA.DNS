using KzA.DNS.Packet;
using System;
using System.Buffers.Binary;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KzA.DNS.DataModel
{
    public class SrvData : IRecordData
    {
        public ushort Priority { get; set; }
        public ushort Weight { get; set; }
        public ushort Port { get; set; }
        public HostData Target { get; set; } = new("srv");

        public string ToZoneFile()
        {
            return $"{Priority} {Weight} {Port} {Target}";
        }

        public static SrvData Parse(ReadOnlySpan<byte> data, int offset, ushort length)
        {
            SrvData srv = new()
            {
                Priority = BinaryPrimitives.ReadUInt16BigEndian(data.Slice(offset, 2)),
                Weight = BinaryPrimitives.ReadUInt16BigEndian(data.Slice(offset + 2, 2)),
                Port = BinaryPrimitives.ReadUInt16BigEndian(data.Slice(offset + 4, 2)),
            };
            offset += 6;
            srv.Target.HostNameRaw = DomainName.Parse(data, ref offset);
            return srv;
        }
    }
}
