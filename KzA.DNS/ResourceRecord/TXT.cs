using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KzA.DNS.DataModel;

namespace KzA.DNS.ResourceRecord
{
    public class TXT : RecordBase<TxtData>
    {
        private TxtData data = new();
        public override string Name { get; set; } = string.Empty;
        public override string? ZoneName { get; set; }
        public override RRType Type { get; } = RRType.TXT;
        public override TxtData Data { get => data; set => data = value; }

        public string Txt { get => data.Txt; set => data.Txt = value; }

        public override string ToString()
        {
            return data.Txt;
        }
    }
}
