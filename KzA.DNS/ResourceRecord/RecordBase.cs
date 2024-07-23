using KzA.DNS.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KzA.DNS.ResourceRecord
{
    public abstract class RecordBase<T> : IRecord
    {
        public abstract string Name { get; set; }
        public abstract string? ZoneName { get; set; }
        public int TTL { get; set; } = -1;
        public abstract RRType Type { get; }
        public abstract T Data { get; set; }
        public override abstract string ToString();
        public virtual string ToZoneFile(bool OmitName = false) =>
            $"{(OmitName ? "" : Name),-15} {(TTL > -1 ? $"{TTL}" : ""),-10} IN    {Type,-7} {((Data is IRecordData RData) ? RData.ToZoneFile() : Data!.ToString())}";
    }
}
