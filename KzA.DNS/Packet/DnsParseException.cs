using System;
using System.Collections.Generic;
using System.Text;

namespace KzA.DNS.Packet
{
    public class DnsParseException : Exception
    {
        public int Offset { get; }
        public IEnumerable<byte> PacketData { get; }
        public DnsParseException(string message, int offset, IEnumerable<byte> data, Exception? innerException = null) : base(message, innerException)
        { Offset = offset; PacketData = data; }
    }
}
