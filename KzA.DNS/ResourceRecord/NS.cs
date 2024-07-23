using KzA.DNS.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KzA.DNS.ResourceRecord
{
    public class NS : RecordBase<HostData>
    {
        private HostData data = new("ns.");
        public override string Name { get; set; } = "@";
        public override string? ZoneName { get; set; }
        public override RRType Type { get; } = RRType.NS;
        public override HostData Data { get => data; set => data = value; }

        public string HostName { get => data.HostName; set => data.HostName = value; }

        public override string ToString()
        {
            return data.HostName;
        }
    }
}
