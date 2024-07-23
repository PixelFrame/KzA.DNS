using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KzA.DNS.ResourceRecord
{
    public interface IRecord
    {
        public string Name { get; set; }
        public string? ZoneName { get; set; }
        public int TTL { get; set; }
        public RRType Type { get; }
        public string ToZoneFile(bool OmitName = false);
    }
}
