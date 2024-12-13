using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KzA.DNS.DataModel
{
    public class UnknownData : IRecordData
    {
        private byte[] _data = [];

        public byte[] Data
        {
            get { return _data; }
            set { _data = value; }
        }

        public string ToZoneFile()
        {
            throw new NotImplementedException();
        }

        public static UnknownData Parse(ReadOnlySpan<byte> data, int offset, ushort length)
        {
            return new UnknownData()
            {
                Data = data.Slice(offset, length).ToArray(),
            };
        }

        public override string ToString()
        {
            return string.Join(' ', _data.Select(b => b.ToString("X2")));
        }
    }
}
