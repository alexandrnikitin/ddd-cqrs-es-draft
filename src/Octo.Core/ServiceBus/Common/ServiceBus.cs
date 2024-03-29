﻿using Octo.Core.ServiceBus.Chains.Publish;
using Octo.Core.ServiceBus.Chains.Receive;
using Octo.Core.ServiceBus.Chains.Send;

namespace Octo.Core.ServiceBus.Common
{
    internal class ServiceBus : IServiceBus
    {
        private readonly IPublishMessageChain _publishMessageChain;

        private readonly IReceiveMessageChain _receiveMessageChain;
        private readonly ISendMessageChain _sendMessageChain;

        public ServiceBus(
            IPublishMessageChain publishMessageChain, 
            ISendMessageChain sendMessageChain, 
            IReceiveMessageChain receiveMessageChain)
        {
            _publishMessageChain = publishMessageChain;
            _sendMessageChain = sendMessageChain;
            _receiveMessageChain = receiveMessageChain;
        }

        public void Publish<TMessage>(TMessage message) where TMessage : IMessage
        {
            _publishMessageChain.Publish(message);
        }

        public void Receive()
        {
            _receiveMessageChain.Receive();
        }

        public void Send<TMessage>(TMessage message) where TMessage : IMessage
        {
            _sendMessageChain.Send(message);
        }
    }
}