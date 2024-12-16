using KzA.DNS.Packet;
using System;
using System.Buffers.Binary;
using System.Collections.Generic;
using System.Text;

namespace KzA.DNS.DataModel
{
    public class TSigData : IRecordData
    {
        private DomainName AlgorithmName = new();
        private DateTime TimeSigned;
        private ushort Fudge;
        private ushort MacSize;
        private byte[] MAC = [];
        private ushort OriginalID;
        private RCODE Error; // RCODE enum is 1-byte, although Error in TSIG is 2-byte
        private ushort OtherLen;
        private byte[] OtherData = [];
        private DateTime ServerCurrentTime;

        public string ToZoneFile()
        {
            throw new NotImplementedException();
        }

        public static TSigData Parse(ReadOnlySpan<byte> data, int offset, int length)
        {
            var tsig = new TSigData();
            tsig.AlgorithmName = DomainName.Parse(data, ref offset);
            tsig.TimeSigned = DateTime.UnixEpoch.AddSeconds((BinaryPrimitives.ReadUInt64BigEndian(data.Slice(offset, 8)) & 0xFFFFFFFFFFFF0000) >> 16).ToLocalTime(); // 48-bit integer
            offset += 6;
            tsig.Fudge = BinaryPrimitives.ReadUInt16BigEndian(data.Slice(offset, 2));
            offset += 2;
            tsig.MacSize = BinaryPrimitives.ReadUInt16BigEndian(data.Slice(offset, 2));
            offset += 2;
            tsig.MAC = data.Slice(offset, tsig.MacSize).ToArray();
            offset += tsig.MacSize;
            tsig.OriginalID = BinaryPrimitives.ReadUInt16BigEndian(data.Slice(offset, 2));
            offset += 2;
            tsig.Error = (RCODE)BinaryPrimitives.ReadUInt16BigEndian(data.Slice(offset, 2));
            offset += 2;
            tsig.OtherLen = BinaryPrimitives.ReadUInt16BigEndian(data.Slice(offset, 2));
            offset += 2;
            if (tsig.OtherLen > 0)
            {
                tsig.OtherData = data.Slice(offset, tsig.OtherLen).ToArray();
                if (tsig.Error == RCODE.BADTIME)
                {
                    // We don't have enough bytes to be read as ulong, have to manually parse the 48bit integer here...
                    ulong high = BinaryPrimitives.ReadUInt32BigEndian(data.Slice(offset, 4)) << 16;
                    ulong low = BinaryPrimitives.ReadUInt16BigEndian(data.Slice(offset + 4, 2));
                    tsig.ServerCurrentTime = DateTime.UnixEpoch.AddSeconds(high | low).ToLocalTime();
                }
            }
            return tsig;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"AlgorithmName: {AlgorithmName}")
              .AppendLine($"TimeSigned: {TimeSigned:O}")
              .AppendLine($"Fudge: {Fudge}")
              .AppendLine($"MAC: ({MacSize}) {string.Join(' ', MAC.Select(b => b.ToString("X2")))}")
              .AppendLine($"OriginalID: {OriginalID:X4}")
              .AppendLine($"Error: {Error}")
              .Append($"OtherData: ({OtherLen}) {string.Join(' ', OtherData.Select(b => b.ToString("X2")))}");
            if (Error == RCODE.BADTIME)
            {
                sb.AppendLine()
                  .Append($"ServerCurrentTime: {ServerCurrentTime}");
            }
            return sb.ToString();
        }
    }
}
