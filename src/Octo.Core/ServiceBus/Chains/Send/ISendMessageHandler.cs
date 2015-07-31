using Octo.Core.ServiceBus.Common;

namespace Octo.Core.ServiceBus.Chains.Send
{
    internal interface ISendMessageHandler : IHandler<MessageContext>
    {
    }
}