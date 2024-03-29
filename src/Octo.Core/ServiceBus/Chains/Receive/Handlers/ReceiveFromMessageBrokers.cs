﻿using Octo.Core.ServiceBus.Subscriptions;

namespace Octo.Core.ServiceBus.Chains.Receive.Handlers
{
    internal class ReceiveFromMessageBrokers : IReceiveMessageHandler
    {
        private readonly IMessageBrokerManager _messageBrokerManager;

        public ReceiveFromMessageBrokers(IMessageBrokerManager messageBrokerManager)
        {
            _messageBrokerManager = messageBrokerManager;
        }

        public void Handle(ReceiveMessageContext context)
        {
            var messageBrokers = _messageBrokerManager.GetReceivers();

            foreach (var messageBroker in messageBrokers)
            {
                messageBroker.Receive();
            }
        }
    }
}