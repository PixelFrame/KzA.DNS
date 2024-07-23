using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KzA.DNS.DataModel
{
    public class SoaData : IRecordData
    {
        public HostData MNAME { get; set; } = new();
        public HostData RNAME { get; set; } = new();
        public uint SERIAL { get; set; }
        public uint REFRESH { get; set; }
        public uint RETRY { get; set; }
        public uint EXPIRE { get; set; }
        public uint MINIMUM {  get; set; }

        public string ToZoneFile()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"{MNAME} {RNAME}(")
              .AppendLine($"                                         {SERIAL, -10};Serial")
              .AppendLine($"                                         {REFRESH, -10};Refresh")
              .AppendLine($"                                         {RETRY, -10};Retry")
              .AppendLine($"                                         {EXPIRE, -10};Expire")
              .AppendLine($"                                         {MINIMUM, -10};Minimum")
              .AppendLine( "                                         )");
            return sb.ToString();
        }
    }
}
