namespace Octo.ServiceBus.ServiceBus.Chains.Send
{
    internal interface ISendMessageChain
    {
        void Send(IMessage message);
    }
}