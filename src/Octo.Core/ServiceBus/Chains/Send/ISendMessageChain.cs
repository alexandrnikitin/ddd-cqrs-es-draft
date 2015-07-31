namespace Octo.Core.ServiceBus.Chains.Send
{
    internal interface ISendMessageChain
    {
        void Send(IMessage message);
    }
}