using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KzA.DNS.ResourceRecord
{
    public class CNAME : RecordBase<string>
    {
        private string data = string.Empty;
        public override string Name { get; set; } = string.Empty;
        public override string? ZoneName { get; set; }
        public override RRType Type { get; } = RRType.CNAME;
        public override string Data { get => data; set => data = value; }

        public override string ToString()
        {
            return data;
        }
    }
}
