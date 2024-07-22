using KzA.DNS.ResourceRecord;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KzA.DNS.Zone
{
    public class PrimaryZone
    {
        public string Name { get; set; } = ".";
        public readonly SOA SOA = new();
        public readonly List<IRecord> Records = [];
        public uint DefaultTTL { get; set; } = 3600;

        public string ToZoneFile()
        {
            var sb = new StringBuilder();
            sb.AppendLine($";Zone file of primary zone: {Name}");
            sb.AppendLine();
            sb.AppendLine($"$TTL {DefaultTTL}");
            sb.AppendLine();
            sb.AppendLine(";NAME           TTL        CLASS TYPE RDATA");
            sb.AppendLine(SOA.ToZoneFile());
            foreach (var record in Records)
            {
                sb.AppendLine(record.ToZoneFile());
            }
            return sb.ToString();
        }
    }
}
