using KzA.DNS.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KzA.DNS.ResourceRecord
{
    public class SVCB : RecordBase<SvcbData>
    {
        private SvcbData data = new();
        public override string Name { get; set; } = "_svc";
        public override string? ZoneName { get; set; }
        public override RRType Type { get; } = RRType.SVCB;
        public override SvcbData Data { get => data; set => data = value; }

        public ushort Priority { get => data.Priority; set => data.Priority = value; }
        public string Target { get => data.Target.HostName; set => data.Target.HostName = value; }
        public Dictionary<string, string> Params { get => data.Params; }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("Priority: ").AppendLine(Priority.ToString())
              .Append("Target: ").AppendLine(Target)
              .AppendLine("Params:");
            foreach (var param in Params)
            {
                sb.Append("  ").Append(param.Key).Append('=').AppendLine(param.Value);
            }
            return sb.ToString();
        }
    }
}
