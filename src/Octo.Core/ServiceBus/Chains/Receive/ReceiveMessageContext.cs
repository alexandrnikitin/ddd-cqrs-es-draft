using System;
using System.Collections.Generic;

using Octo.Core.ServiceBus.Common;

namespace Octo.Core.ServiceBus.Chains.Receive
{
    public class ReceiveMessageContext : MessageContext
    {
        public ReceiveMessageContext(Context parentContext)
            : base(parentContext)
        {
        }

        public Type MessageBrokerType { get; set; }

        public List<IMessageHandlerContainer> MessageHandlerContainers
        {
            get { return Get<List<IMessageHandlerContainer>>(); }

            set { Set(value); }
        }

        public bool TryGetMessageEnvelop(out IMessageEnvelop messageEnvelop)
        {
            return TryGet<IMessageEnvelop>(out messageEnvelop);
        }
    }
}