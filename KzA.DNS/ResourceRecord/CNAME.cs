using KzA.DNS.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KzA.DNS.ResourceRecord
{
    public class CNAME : RecordBase<HostData>
    {
        private HostData data = new();
        public override string Name { get; set; } = string.Empty;
        public override string? ZoneName { get; set; }
        public override RRType Type { get; } = RRType.CNAME;
        public override HostData Data { get => data; set => data = value; }
        
        public string HostName { get => data.HostName; set => data.HostName = value; }

        public override string ToString()
        {
            return data.HostName;
        }
    }
}
