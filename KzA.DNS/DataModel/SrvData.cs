using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KzA.DNS.DataModel
{
    public class SrvData : IRecordData
    {
        public ushort Priority { get; set; }
        public ushort Weight { get; set; }
        public ushort Port { get; set; }
        public HostData Target { get; set; } = new("srv");

        public string ToZoneFile()
        {
            return $"{Priority} {Weight} {Port} {Target}";
        }
    }
}
