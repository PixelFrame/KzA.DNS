using KzA.DNS.Packet;
using KzA.DNS.ResourceRecord;
using System;
using System.Buffers.Binary;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;

namespace KzA.DNS.DataModel
{
    public class NSec3Data : IRecordData
    {
        private DnsSecNSec3HashAlgorithm HashAlgorithm;
        private DnsSecNSec3Flags Flags;
        private ushort Iterations;
        private byte SaltLength;
        private byte[] Salt = [];
        private byte HashLength;
        private byte[] NextHashedOwnerName = [];
        private List<RRType> TypeBitMaps = [];

        public string ToZoneFile()
        {
            throw new NotImplementedException();
        }

        public static NSec3Data Parse(ReadOnlySpan<byte> data, int offset, int length)
        {
            var end = offset + length;
            var result = new NSec3Data
            {
                HashAlgorithm = (DnsSecNSec3HashAlgorithm)data[offset],
                Flags = (DnsSecNSec3Flags)data[offset + 1],
                Iterations = BinaryPrimitives.ReadUInt16BigEndian(data.Slice(offset + 2, 2)),
                SaltLength = data[offset + 4]
            };
            offset += 5;
            result.Salt = data.Slice(offset, result.SaltLength).ToArray();
            offset += result.SaltLength;
            result.HashLength = data[offset++];
            result.NextHashedOwnerName = data.Slice(offset, result.HashLength).ToArray();
            offset += result.HashLength;
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

            var sb = new StringBuilder();
            sb.AppendLine($"HashAlgorithm: {HashAlgorithm}")
              .AppendLine($"Flags: {Flags}")
              .AppendLine($"Iterations: {Iterations}")
              .AppendLine($"Salt: ({SaltLength}) {string.Join(' ', Salt.Select(b => b.ToString("X2")))}")
              .AppendLine($"NextHashedOwnerName: ({HashLength}) {string.Join(' ', NextHashedOwnerName.Select(b => b.ToString("X2")))}")
              .Append($"TypeBitMaps: {string.Join(", ", TypeBitMaps)}");
            return sb.ToString();
        }
    }
}
