using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KzA.DNS.Packet
{
    public struct DomainName
    {
        public Label[] Labels;
        public byte Terminator;

        public override string ToString()
        {
            return string.Join(".", Labels) + '.';
        }
    }

    public struct Label
    {
        public byte Count;
        public byte[] Data;

        public override string ToString()
        {
            return Encoding.ASCII.GetString(Data);
        }
    }
}
