using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KzA.DNS.Packet
{
    public struct Question
    {
        public DomainName QName;
        public RRType QType;
        public Class QClass;
    }
}
