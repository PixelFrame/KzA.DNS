using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KzA.DNS.DataModel
{
    public class MxData : IRecordData
    {
        public ushort Preference { get; set; }
        public HostData Host { get; set; } = new("mx.");

        public string ToZoneFile()
        {
            return $"{Preference} {Host}";
        }
    }
}
