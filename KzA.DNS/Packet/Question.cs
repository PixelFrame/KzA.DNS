using System;
using System.Buffers.Binary;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace KzA.DNS.Packet
{
    public class Question
    {
        public DomainName QName = new();
        public RRType QType;
        public Class QClass;

        public static Question Parse(ReadOnlySpan<byte> data, ref int offset)
        {
            Question q = new()
            {
                QName = DomainName.Parse(data, ref offset),
                QType = (RRType)BinaryPrimitives.ReadUInt16BigEndian(data.Slice(offset, 2)),
                QClass = (Class)BinaryPrimitives.ReadUInt16BigEndian(data.Slice(offset + 2, 2)),
            };
            offset += 4;
            return q;
        }
    }
}
