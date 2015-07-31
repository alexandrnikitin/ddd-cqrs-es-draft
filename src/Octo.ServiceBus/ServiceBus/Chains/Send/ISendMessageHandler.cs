using Octo.ServiceBus.ServiceBus.Common;

namespace Octo.ServiceBus.ServiceBus.Chains.Send
{
    internal interface ISendMessageHandler : IHandler<MessageContext>
    {
    }
}