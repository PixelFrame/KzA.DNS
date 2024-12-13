using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace KzA.DNS.ResourceRecord
{
    public class ArbitraryRecord : RecordBase<string>
    {
        private RRType type;
        private string data = string.Empty;
        public override string Name { get; set; } = string.Empty;
        public override string? ZoneName { get; set; }
        public override RRType Type { get => type; }

        public void ModifyType(RRType type)
        {
            this.type = type;
        }

        public override string Data
        {
            get => data;
            set => data = value;
        }

        public override string ToString()
        {
            return data.ToString();
        }
    }
}
