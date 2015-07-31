using System.Collections.Generic;

using Octo.ServiceBus.ServiceBus.Chains.Receive;

namespace Octo.ServiceBus.ServiceBus.MessageBrokers.AzureServiceBus.Chains.Receive
{
    internal class AzureReceiveMessageChain
        : ChainOfHandlers<ReceiveMessageContext, IReceiveMessageHandler>, IAzureReceiveMessageChain
    {
        public AzureReceiveMessageChain(
            IHandlingStrategy<ReceiveMessageContext, IReceiveMessageHandler> handlingStrategy, 
            IEnumerable<IReceiveMessageHandler> handlers)
            : base(handlingStrategy, handlers)
        {
        }

        public void Receive(ReceiveMessageContext context)
        {
            Process(context);
        }
    }
}