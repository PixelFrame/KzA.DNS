using KzA.DNS.Packet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KzA.DNS.DataModel
{
    public class HostData : IRecordData
    {
        private DomainName hostname = new(".");
        public string HostName
        {
            get => hostname.ToString();
            set
            {
                if (Uri.CheckHostName(value) == UriHostNameType.Dns)
                {
                    hostname = new(value);
                }
                else if (Uri.CheckHostName(value.Replace('_', 'a')) == UriHostNameType.Dns)  // Uri.CheckHostName does not recognize DNS labels beginning with _, which is weird...
                {
                    hostname = new(value);
                }
                else
                {
                    throw new ArgumentException("Not valid DNS hostname");
                }
            }
        }
        internal DomainName HostNameRaw
        {
            get => hostname;
            set => hostname = value;
        }

        public HostData() { }
        public HostData(string hostname)
        {
            HostName = hostname;
        }

        public string ToZoneFile()
        {
            return ToString();
        }

        public override string ToString()
        {
            return hostname.ToString();
        }

        public static HostData Parse(ReadOnlySpan<byte> data, int offset, ushort length)
        {
            return new()
            {
                hostname = DomainName.Parse(data, ref offset)
            };
        }
    }
}