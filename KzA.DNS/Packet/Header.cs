using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace KzA.DNS.Packet
{
    [StructLayout(LayoutKind.Explicit)]
    public struct Header
    {
        [FieldOffset(0)]
        public ushort TransactionID;

        [FieldOffset(2)]
        public ushort Flags;

        [FieldOffset(4)]
        public ushort QuestionCount;

        [FieldOffset(6)]
        public ushort AnswerCount;

        [FieldOffset(8)]
        public ushort AuthorityCount;

        [FieldOffset(10)]
        public ushort AdditionalCount;

        public readonly HeaderFlags HeaderFlags => (HeaderFlags)(Flags & 0x87F0);
        public readonly OpCode OpCode => (OpCode)(Flags & 0x7800 >> 11);
        public readonly RCODE RCODE => (RCODE)(Flags & 0x000F);
    }
}
