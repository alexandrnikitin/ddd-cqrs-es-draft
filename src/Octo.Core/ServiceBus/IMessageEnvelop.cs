using System;
using System.Collections.Generic;

using Microsoft.ServiceBus.Messaging;

namespace Octo.Core.ServiceBus
{
    public interface IMessageEnvelop
    {
        IDictionary<string, string> Headers { get; set; }

        IMessage Message { get; set; }

        Type MessageType { get; set; }

        BrokeredMessage ToBrokeredMessage();
    }
}