using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Octo.ServiceBus.ServiceBus.Common
{
    [Serializable]
    internal class MessageEnvelop : IMessageEnvelop
    {
        public MessageEnvelop(IMessage message)
        {
            Message = message;
            MessageType = message.GetType();
        }

        public IDictionary<string, string> Headers { get; set; }

        public IMessage Message { get; set; }

        public Type MessageType { get; set; }

        // todo 1341 make it transport specific
        public static MessageEnvelop FromBrokeredMessage(BrokeredMessage brokeredMessage)
        {
            if (brokeredMessage == null) throw new ArgumentNullException("brokeredMessage");

            var messageBody = brokeredMessage.GetBody<byte[]>();
            using (var memoryStream = new MemoryStream(messageBody))
            {
                var formatter = new BinaryFormatter();
                var message = (MessageEnvelop)formatter.Deserialize(memoryStream);
                return message;
            }
        }

        public BrokeredMessage ToBrokeredMessage()
        {
            using (var memoryStream = new MemoryStream())
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(memoryStream, this);
                memoryStream.Position = 0;

                return new BrokeredMessage(memoryStream.ToArray());
            }
        }
    }
}