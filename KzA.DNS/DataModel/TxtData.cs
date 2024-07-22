using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KzA.DNS.DataModel
{
    public class TxtData
    {
        public string Txt = string.Empty;

        public override string ToString()
        {
            return $"\"{Txt}\"";
        }
    }
}
