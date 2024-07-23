using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KzA.DNS.DataModel
{
    public class TxtData : IRecordData
    {
        public string Txt = string.Empty;

        public string ToZoneFile()
        {
            return $"\"{Txt}\"";
        }
    }
}
