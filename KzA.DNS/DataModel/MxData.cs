using System;
using System.Buffers.Binary;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KzA.DNS.DataModel
{
    public class MxData : IRecordData
    {
        public ushort Preference { get; set; }
        public HostData Host { get; set; } = new("mx.");

        public string ToZoneFile()
        {
            return $"{Preference} {Host}";
        }

        public static MxData Parse(ReadOnlySpan<byte> data, int offset, ushort length)
        {
            return new()
            {
                Preference = BinaryPrimitives.ReadUInt16BigEndian(data.Slice(offset, 2)),
                Host = HostData.Parse(data, offset + 2, length), // Should be (length - 2), but domain name parsing does not rely on length so omit here
            };
        }
    }
}
