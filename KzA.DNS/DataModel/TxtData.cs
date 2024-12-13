using KzA.DNS.Packet;
using System;
using System.Buffers.Binary;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KzA.DNS.DataModel
{
    public class TxtData : IRecordData
    {
        public string Txt = string.Empty;

        public string ToZoneFile()
        {
            return $"\"{Txt}\"";
        }
        public static TxtData Parse(ReadOnlySpan<byte> data, int offset, ushort length)
        {
            return new()
            {
                Txt = Encoding.UTF8.GetString(data.Slice(offset, length)),
            };
        }

        public override string ToString()
        {
            return Txt;
        }
    }
}
