using KzA.DNS.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace KzA.DNS.ResourceRecord
{
    public class SRV : RecordBase<SrvData>
    {
        private SrvData data = new();
        public override string Name { get; set; } = "_srv._tcp";
        public override string? ZoneName { get; set; }
        public override RRType Type { get; } = RRType.SRV;
        public override SrvData Data { get => data; set => data = value; }

        public ushort Priority { get => data.Priority; set => data.Priority = value; }
        public ushort Weight { get => data.Weight; set => data.Weight = value; }
        public ushort Port { get => data.Port; set => data.Port = value; }
        public string Target { get => data.Target.HostName; set => data.Target.HostName = value; }

        public override string ToString()
        {
            return
$@"Priority: {Priority}
Weight: {Weight}
Port: {Port}
Target: {Target}";
        }
    }
}
