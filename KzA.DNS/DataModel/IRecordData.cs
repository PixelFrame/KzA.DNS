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
        public static IRecordData Parse(ReadOnlySpan<byte> data, int offset, ushort length)
        {
            return new UnknownData()
            {
                Data = data.Slice(offset, length).ToArray(),
            };
        }
    }
}
