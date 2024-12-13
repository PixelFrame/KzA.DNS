using KzA.DNS.Packet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace KzA.DNS.DataModel
{
    public class AddressData : IRecordData
    {
        private IPAddress address = IPAddress.Any;
        public IPAddress Address
        {
            get => address;
            set => address = value;
        }

        public AddressData() { }
        public AddressData(string addressStr)
        {
            address = IPAddress.Parse(addressStr);
        }

        public string ToZoneFile()
        {
            return address.ToString();
        }

        public override string ToString()
        {
            return address.ToString();
        }

        public static AddressData Parse(ReadOnlySpan<byte> data, int offset, ushort length)
        {
            return new()
            {
                address = new IPAddress(data.Slice(offset, length)),
            };
        }
    }
}
