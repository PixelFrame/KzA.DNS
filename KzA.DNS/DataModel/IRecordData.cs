using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KzA.DNS.DataModel
{
    public interface IRecordData
    {
        public string ToZoneFile();
    }
}
