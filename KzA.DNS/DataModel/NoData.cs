using System;
using System.Collections.Generic;
using System.Text;

namespace KzA.DNS.DataModel
{
    public class NoData : IRecordData
    {
        public string ToZoneFile()
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            return string.Empty;
        }
    }
}
