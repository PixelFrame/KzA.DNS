using KzA.DNS.DataModel;
using System;
using System.Buffers.Binary;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KzA.DNS.Packet
{
    public class Answer
    {
        public DomainName Name = new();
        public RRType Type;
        public Class Class;
        public ushort EDNS_UdpPayloadSize => (ushort)Class;
        public uint TTL;
        public byte EDNS_HighRCODE => (byte)(TTL >> 24);
        public byte EDNS_Version => (byte)(TTL & 0x00FF0000 >> 16);
        public bool EDNS_DO => (TTL & 0x00008000) != 0;
        public ushort RDLength;
        public IRecordData RData = new UnknownData();

        public static Answer Parse(ReadOnlySpan<byte> data, ref int offset)
        {
            Answer a = new()
            {
                Name = DomainName.Parse(data, ref offset),
                Type = (RRType)BinaryPrimitives.ReadUInt16BigEndian(data.Slice(offset, 2)),
                Class = (Class)BinaryPrimitives.ReadUInt16BigEndian(data.Slice(offset + 2, 2)),
                TTL = BinaryPrimitives.ReadUInt32BigEndian(data.Slice(offset + 4, 4)),
                RDLength = BinaryPrimitives.ReadUInt16BigEndian(data.Slice(offset + 8, 2)),
            };
            offset += 10;

            a.RData = a.Type switch
            {
                RRType.A or
                RRType.AAAA => AddressData.Parse(data, offset, a.RDLength),
                RRType.CNAME or
                RRType.PTR => HostData.Parse(data, offset, a.RDLength),
                RRType.MX => MxData.Parse(data, offset, a.RDLength),
                RRType.SOA => SoaData.Parse(data, offset, a.RDLength),
                RRType.SRV => SrvData.Parse(data, offset, a.RDLength),
                RRType.TXT => TxtData.Parse(data, offset, a.RDLength),
                RRType.SVCB => SvcbData.Parse(data, offset, a.RDLength),
                _ => UnknownData.Parse(data, offset, a.RDLength),
            };
            offset += a.RDLength;
            return a;
        }
    }
}
