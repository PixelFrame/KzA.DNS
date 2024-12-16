using KzA.DNS.Packet;
using System;
using System.Buffers.Binary;
using System.Collections.Generic;
using System.Text;

namespace KzA.DNS.DataModel
{
    public class TKeyData : IRecordData
    {
        private DomainName AlgorithmName = new();
        private DateTime Inception;
        private DateTime Expiration;
        private TKeyMode Mode;
        private RCODE Error; // RCODE enum is 1-byte, although Error in TSIG is 2-byte
        private ushort KeySize;
        private byte[] Key = [];
        private ushort OtherSize;
        private byte[] OtherData = [];

        public string ToZoneFile()
        {
            throw new NotImplementedException();
        }

        public static TKeyData Parse(ReadOnlySpan<byte> data, int offset, int length)
        {
            var tkey = new TKeyData();
            tkey.AlgorithmName = DomainName.Parse(data, ref offset);
            tkey.Inception = DateTime.UnixEpoch.AddSeconds(BinaryPrimitives.ReadUInt32BigEndian(data.Slice(offset, 4))).ToLocalTime();
            offset += 4;
            tkey.Expiration = DateTime.UnixEpoch.AddSeconds(BinaryPrimitives.ReadUInt32BigEndian(data.Slice(offset, 4))).ToLocalTime();
            offset += 4;
            tkey.Mode = (TKeyMode)BinaryPrimitives.ReadUInt16BigEndian(data.Slice(offset, 2));
            offset += 2;
            tkey.Error = (RCODE)BinaryPrimitives.ReadUInt16BigEndian(data.Slice(offset, 2));
            offset += 2;
            tkey.KeySize = BinaryPrimitives.ReadUInt16BigEndian(data.Slice(offset, 2));
            offset += 2;
            tkey.Key = data.Slice(offset, tkey.KeySize).ToArray();
            offset += tkey.KeySize;
            tkey.OtherSize = BinaryPrimitives.ReadUInt16BigEndian(data.Slice(offset, 2));
            offset += 2;
            if (tkey.OtherSize > 0)
            {
                tkey.OtherData = data.Slice(offset, tkey.OtherSize).ToArray();
            }
            return tkey;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"AlgorithmName: {AlgorithmName}")
              .AppendLine($"Inception: {Inception:O}")
              .AppendLine($"Expiration: {Expiration:O}")
              .AppendLine($"Mode: {Mode}")
              .AppendLine($"Error: {Error}")
              .AppendLine($"Key: ({KeySize}) {string.Join(' ', Key.Select(b => b.ToString("X2")))}")
              .Append($"OtherData: ({OtherSize}) {string.Join(' ', OtherData.Select(b => b.ToString("X2")))}");
            return sb.ToString();
        }
    }
}
