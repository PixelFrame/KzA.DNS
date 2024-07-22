using KzA.DNS.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KzA.DNS.ResourceRecord
{
    public class MX : RecordBase<MxData>
    {
        private MxData data = new();
        public override string Name { get; set; } = "mx";
        public override string? ZoneName { get; set; }
        public override RRType Type { get; } = RRType.MX;
        public override MxData Data { get => data; set => data = value; }

        public ushort Preference { get => data.Preference; set => data.Preference = value; }
        public string Host { get => data.Host; set => data.Host = value; }

        public override string ToString()
        {
            return
$@"Preference: {Preference}
Host: {Host}";
        }
    }
}
