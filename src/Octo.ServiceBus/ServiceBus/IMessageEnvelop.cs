using System;
using System.Collections.Generic;

namespace Octo.ServiceBus.ServiceBus
{
    public interface IMessageEnvelop
    {
        IDictionary<string, string> Headers { get; set; }

        IMessage Message { get; set; }

        Type MessageType { get; set; }

        BrokeredMessage ToBrokeredMessage();
    }
}