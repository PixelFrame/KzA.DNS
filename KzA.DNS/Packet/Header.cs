using System;
using System.Buffers.Binary;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KzA.DNS.Packet
{
    public struct Header
    {
        public ushort TransactionID;
        public ushort Flags;
        public ushort QuestionCount;
        public ushort AnswerCount;
        public ushort AuthorityCount;
        public ushort AdditionalCount;

        public readonly ushort ZoneCount => QuestionCount;
        public readonly ushort PrerequisiteCount => AnswerCount;
        public readonly ushort UpdateCount => AuthorityCount;

        public readonly HeaderFlags HeaderFlags => (HeaderFlags)(Flags & 0x87F0);
        public readonly OpCode OpCode => (OpCode)(Flags & 0x7800 >> 11);
        public readonly RCODE RCODE => (RCODE)(Flags & 0x000F);

        public static Header Parse(ReadOnlySpan<byte> data, ref int offset)
        {
            Header header = new()
            {
                TransactionID = BinaryPrimitives.ReadUInt16BigEndian(data.Slice(offset, 2)),
                Flags = BinaryPrimitives.ReadUInt16BigEndian(data.Slice(offset + 2, 2)),
                QuestionCount = BinaryPrimitives.ReadUInt16BigEndian(data.Slice(offset + 4, 2)),
                AnswerCount = BinaryPrimitives.ReadUInt16BigEndian(data.Slice(offset + 6, 2)),
                AuthorityCount = BinaryPrimitives.ReadUInt16BigEndian(data.Slice(offset + 8, 2)),
                AdditionalCount = BinaryPrimitives.ReadUInt16BigEndian(data.Slice(offset + 10, 2))
            };
            offset += 12;
            return header;
        }
    }
}
