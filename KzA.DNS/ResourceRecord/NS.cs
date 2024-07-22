using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KzA.DNS.ResourceRecord
{
    public class NS : RecordBase<string>
    {
        private string data = "ns.";
        public override string Name { get; set; } = "@";
        public override string? ZoneName { get; set; }
        public override RRType Type { get; } = RRType.NS;
        public override string Data
        {
            get => data;
            set
            {
                data = value;
                if (!data.EndsWith('.'))
                {
                    data += ".";
                }
            }
        }

        public override string ToString()
        {
            return Data;
        }
    }
}
