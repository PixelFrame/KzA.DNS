using KzA.DNS.Packet;
using KzA.DNS.ResourceRecord;
using System;
using System.Collections.Generic;
using System.Text;

namespace KzA.DNS.DataModel
{
    public class NSecData : IRecordData
    {
        private DomainName NextDomainName = new();
        private List<RRType> TypeBitMaps = [];

        public string ToZoneFile()
        {
            throw new NotImplementedException();
        }

        public static NSecData Parse(ReadOnlySpan<byte> data, int offset, int length)
        {
            var end = offset + length;
            var result = new NSecData
            {
                NextDomainName = DomainName.Parse(data, ref offset)
            };
            while (offset < end)
            {
                ushort winBlock = (ushort)(data[offset++] << 8);
                var blockLen = data[offset++];
                ushort blockCnt = 0;
                while (blockCnt < blockLen)
                {
                    var bit = 0x80;
                    for (byte i = 0; i < 8; i++)
                    {
                        if ((bit & data[offset]) != 0)
                        {
                            result.TypeBitMaps.Add((RRType)(winBlock | (blockCnt * 8 + i)));
                        }
                        bit >>= 1;
                    }
                    blockCnt++;
                    offset++;
                }
            }
            return result;
        }

        public override string ToString()
        {
            return $"NextDomainName: {NextDomainName}{Environment.NewLine}TypeBitMaps: {string.Join(", ", TypeBitMaps)}";
        }
    }
}
