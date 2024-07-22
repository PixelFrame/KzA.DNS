using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace KzA.DNS.ResourceRecord
{
    public class AAAA : RecordBase<IPAddress>
    {
        private IPAddress data = new(0x00000000);
        public override string Name { get; set; } = string.Empty;
        public override string? ZoneName { get; set; }
        public override RRType Type { get; } = RRType.AAAA;
        public override IPAddress Data
        {
            get => data;
            set
            {
                if(value.AddressFamily != System.Net.Sockets.AddressFamily.InterNetworkV6)
                {
                    throw new ArgumentException("Invalid IP Address family");
                }
                data = value;
            }
        }

        public override string ToString()
        {
            return data.ToString();
        }
    }
}
