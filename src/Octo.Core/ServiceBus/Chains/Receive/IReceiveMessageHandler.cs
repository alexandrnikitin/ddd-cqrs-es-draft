using Octo.Core.Patterns.ChainOfHandlers.Interfaces;

namespace Octo.Core.ServiceBus.Chains.Receive
{
    internal interface IReceiveMessageHandler : IHandler<ReceiveMessageContext>
    {
    }
}