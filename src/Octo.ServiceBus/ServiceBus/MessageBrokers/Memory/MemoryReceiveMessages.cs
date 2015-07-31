using Octo.ServiceBus.ServiceBus.Chains.Receive;
using Octo.ServiceBus.ServiceBus.MessageBrokers.Memory.Chains.Receive;

namespace Octo.ServiceBus.ServiceBus.MessageBrokers.Memory
{
    public class MemoryReceiveMessages : IReceiveMessages
    {
        private readonly IContextManager _contextManager;
        private readonly IMemoryReceiveMessageChain _receiveMessageChain;

        public MemoryReceiveMessages(IMemoryReceiveMessageChain receiveMessageChain, IContextManager contextManager)
        {
            _receiveMessageChain = receiveMessageChain;
            _contextManager = contextManager;
        }

        public void Receive()
        {
            var currentContext = _contextManager.Current;
            var context = new ReceiveMessageContext(currentContext) { MessageBrokerType = this.GetType() };
            _receiveMessageChain.Receive(context);
        }
    }
}