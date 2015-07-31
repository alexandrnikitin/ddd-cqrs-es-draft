using Octo.Core.ServiceBus.Chains.Receive;
using Octo.Core.ServiceBus.MessageBrokers.AzureServiceBus.Chains.Receive;

namespace Octo.Core.ServiceBus.MessageBrokers.AzureServiceBus
{
    public class AzureServiceBusReceiveMessages : IReceiveMessages
    {
        private readonly IAzureReceiveMessageChain _azureReceiveMessageChain;
        private readonly IContextManager _contextManager;

        public AzureServiceBusReceiveMessages(
            IAzureReceiveMessageChain azureReceiveMessageChain, 
            IContextManager contextManager)
        {
            _azureReceiveMessageChain = azureReceiveMessageChain;
            this._contextManager = contextManager;
        }

        public void Receive()
        {
            var currentContext = _contextManager.Current;
            var context = new ReceiveMessageContext(currentContext) { MessageBrokerType = this.GetType() };

            _azureReceiveMessageChain.Receive(context);
        }
    }
}