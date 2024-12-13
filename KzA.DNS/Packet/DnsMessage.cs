using System;
using System.Buffers.Binary;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KzA.DNS.Packet
{
    public class DnsMessage
    {
        public Header Header;
        private readonly List<Question> questions = [];
        public List<Question> Questions => questions;
        public List<Question> Zones => questions;
        private readonly List<Answer> answers = [];
        public List<Answer> Answers => answers;
        public List<Answer> Prerequisites => answers;
        private readonly List<Answer> authorities = [];
        public List<Answer> Authorities => authorities;
        public List<Answer> Updates => authorities;
        private readonly List<Answer> additionals = [];
        public List<Answer> Additionals => additionals;
        public ushort TcpMsgLen = 0;

        public static DnsMessage Parse(ReadOnlySpan<byte> data, bool isTCP = false)
        {
            int offset = isTCP ? 2 : 0;
            int qcnt = 0;
            int acnt = 0;
            int aucnt = 0;
            int adcnt = 0;
            DnsMessage message = new()
            {
                TcpMsgLen = isTCP ? BinaryPrimitives.ReadUInt16BigEndian(data[..2]) : (ushort)0,
                Header = Header.Parse(data, ref offset)
            };
            try
            {
                while (qcnt++ < message.Header.QuestionCount)
                {
                    message.questions.Add(Question.Parse(data, ref offset));
                }
                while (acnt++ < message.Header.AnswerCount)
                {
                    message.answers.Add(Answer.Parse(data, ref offset));
                }
                while (aucnt++ < message.Header.AuthorityCount)
                {
                    message.authorities.Add(Answer.Parse(data, ref offset));
                }
                while (adcnt++ < message.Header.AdditionalCount)
                {
                    message.additionals.Add(Answer.Parse(data, ref offset));
                }
            }
            catch (ArgumentOutOfRangeException)
            {
                // Possibel incomplete DNS message, can still move on with partial result
            }
            catch (Exception e)
            {
                throw new DnsParseException($"Failed to parse DNS packet at {offset}", offset, data.ToArray(), e);
            }
            return message;
        }
    }
}
