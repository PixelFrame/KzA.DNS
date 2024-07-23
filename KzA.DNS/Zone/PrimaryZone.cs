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

        public string ToZoneFile(bool SortRecords = false)
        {
            var sb = new StringBuilder();
            sb.AppendLine($";Zone file of primary zone: {Name}");
            sb.AppendLine();
            sb.AppendLine($"$TTL {DefaultTTL}");
            sb.AppendLine();
            sb.AppendLine(";NAME           TTL        CLASS TYPE    RDATA");
            sb.AppendLine(SOA.ToZoneFile());

            var lastName = string.Empty;
            if (SortRecords)
            {
                var ordered = Records.OrderBy(r => r.Type);
                foreach (var record in ordered)
                {
                    sb.AppendLine(record.ToZoneFile(record.Name == lastName));
                    lastName = record.Name;
                }
            }
            else
            {
                foreach (var record in Records)
                {
                    sb.AppendLine(record.ToZoneFile(record.Name == lastName));
                    lastName = record.Name;
                }
            }
            return sb.ToString();
        }
    }
}
