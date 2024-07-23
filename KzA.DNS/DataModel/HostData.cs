using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KzA.DNS.DataModel
{
    public class HostData : IRecordData
    {
        private string hostname = string.Empty;
        public string HostName
        {
            get => hostname;
            set
            {
                if (Uri.CheckHostName(value) == UriHostNameType.Dns)
                {
                    hostname = value;
                    if (!hostname.EndsWith('.')) hostname += '.';
                }
                else
                {
                    throw new ArgumentException("Not valid DNS hostname");
                }
            }
        }

        public HostData() { }
        public HostData(string hostname)
        {
            HostName = hostname;
        }

        public string ToZoneFile()
        {
            return hostname;
        }

        public override string ToString()
        {
            return hostname;
        }
    }
}