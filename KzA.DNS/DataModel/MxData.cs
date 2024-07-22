using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KzA.DNS.DataModel
{
    public class MxData
    {
        public ushort Preference { get; set; }
        public string Host { get; set; } = "mx.";

        public override string ToString()
        {
            return $"{Preference} {Host}";
        }
    }
}
