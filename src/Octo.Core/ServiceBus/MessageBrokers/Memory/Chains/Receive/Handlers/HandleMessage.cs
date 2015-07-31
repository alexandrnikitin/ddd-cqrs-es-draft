using System;

using Octo.Core.ServiceBus.Chains.Receive;

namespace Octo.Core.ServiceBus.MessageBrokers.Memory.Chains.Receive.Handlers
{
    internal class HandleMessage : IReceiveMessageHandler
    {
        private readonly ILogger _logger;

        public HandleMessage(ILogger logger)
        {
            _logger = logger;
        }

        public void Handle(ReceiveMessageContext context)
        {
            IMessageEnvelop messageEnvelop;
            if (!context.TryGetMessageEnvelop(out messageEnvelop)) return;

            var message = messageEnvelop.Message;

            foreach (var messageHandlerContainer in context.MessageHandlerContainers)
            {
                var handlerInstance = messageHandlerContainer.Instance;
                try
                {
                    messageHandlerContainer.Invoke(handlerInstance, message);
                }
                catch (Exception exception)
                {
                    _logger.Error("Error handling message:");
                    _logger.Error(exception);
                }
            }
        }
    }
}