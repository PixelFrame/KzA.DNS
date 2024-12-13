using System;
using System.Buffers.Binary;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace KzA.DNS.Packet
{
    public class DomainName
    {
        public List<Label> Labels = [];
        public byte Terminator = 0;

        public override string ToString()
        {
            return string.Join(".", Labels) + '.';
        }

        public DomainName() { }
        public DomainName(string name)
        {
            foreach (var l in name.Split('.', StringSplitOptions.RemoveEmptyEntries))
            {
                Labels.Add(new(l));
            }
        }

        public static DomainName Parse(ReadOnlySpan<byte> data, ref int offset)
        {
            DomainName domainName = new();
            bool nullTerm = true;
            while (data[offset] != 0)
            {
                if (data[offset] > 0x3F)
                {
                    var ptr = BinaryPrimitives.ReadUInt16BigEndian(data.Slice(offset, 2)) & 0x3FFF;
                    domainName.Labels.AddRange(Parse(data, ref ptr).Labels);
                    offset += 2;
                    nullTerm = false;
                    break;
                }
                else
                {
                    var label = new Label
                    {
                        Count = data[offset]
                    };
                    offset++;
                    label.Data = data.Slice(offset, label.Count).ToArray();
                    offset += label.Count;
                    domainName.Labels.Add(label);
                }
            }
            if (nullTerm) offset++;

            return domainName;
        }
    }

    public class Label
    {
        public byte Count;
        public byte[] Data = [];

        public Label() { }
        public Label(string l)
        {
            if (l.Length > 0x3F) throw new ArgumentException($"{l} is too long as a domain name label");
            Count = (byte)l.Length;
            Data = Encoding.UTF8.GetBytes(l);
        }

        public override string ToString()
        {
            return Encoding.ASCII.GetString(Data);
        }
    }
}
