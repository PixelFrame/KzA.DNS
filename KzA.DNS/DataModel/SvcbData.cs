using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KzA.DNS.DataModel
{
    public class SvcbData : IRecordData
    {
        public ushort Priority { get; set; }
        public HostData Target { get; set; } = new("svc");
        public Dictionary<string, string> Params { get; set; } = [];

        public string ToZoneFile()
        {
            var sb = new StringBuilder();
            sb.Append(Priority).Append(' ').Append(Target).AppendLine(" (");
            foreach (var param in Params)
            {
                if (string.IsNullOrEmpty(param.Value))
                    sb.AppendLine(param.Key);
                else
                    sb.Append("                                         ").Append(param.Key).Append('=').AppendLine(param.Value);
            }
            sb.Append("                                         )");

            return sb.ToString();
        }
    }
}
