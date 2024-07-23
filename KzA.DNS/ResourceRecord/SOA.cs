using KzA.DNS.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KzA.DNS.ResourceRecord
{
    public class SOA : RecordBase<SoaData>
    {
        private SoaData data = new();
        public override string Name { get; set; } = "@";
        public override string? ZoneName { get; set; }
        public override RRType Type { get; } = RRType.SOA;
        public override SoaData Data { get => data; set => data = value; }

        public string MNAME { get => data.MNAME.HostName; set => data.MNAME.HostName = value; }
        public string RNAME { get => data.RNAME.HostName; set => data.RNAME.HostName = value; }
        public uint SERIAL { get => data.SERIAL; set => data.SERIAL = value; }
        public uint REFRESH { get => data.REFRESH; set => data.REFRESH = value; }
        public uint RETRY { get => data.RETRY; set => data.RETRY = value; }
        public uint EXPIRE { get => data.EXPIRE; set => data.EXPIRE = value; }
        public uint MINIMUM { get => data.MINIMUM; set => data.MINIMUM = value; }

        public override string ToString()
        {
            return
$@"Primary server: {MNAME}
Responsible person: {RNAME}
Serial number: {SERIAL}
Refresh interval: {REFRESH}
Retry interval: {RETRY}
Expires after: {EXPIRE}
Minimum TTL: {MINIMUM}";
        }
    }
}
