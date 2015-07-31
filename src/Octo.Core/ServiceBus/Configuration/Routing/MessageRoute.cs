using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Octo.Core.ServiceBus.Configuration.Routing
{
    public class MessageRoute
    {
        public MessageRoute(Type message)
        {
            this.Message = message;
            this.ReceiveFrom = new Collection<MessageBrokerRoute>();
            this.PublishTo = new Collection<Type>();
        }

        public Type Message { get; private set; }

        public ICollection<Type> PublishTo { get; private set; }

        public ICollection<MessageBrokerRoute> ReceiveFrom { get; private set; }
    }
}