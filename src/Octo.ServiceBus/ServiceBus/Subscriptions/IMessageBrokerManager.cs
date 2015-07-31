using System;
using System.Collections.Generic;

using Octo.ServiceBus.ServiceBus.Common;
using Octo.ServiceBus.ServiceBus.MessageBrokers;

namespace Octo.ServiceBus.ServiceBus.Subscriptions
{
    internal interface IMessageBrokerManager
    {
        IEnumerable<MessageHandlerContainer> GetHandlers(Type messageType, Type messageBrokerType);

        IEnumerable<IPublishMessages> GetPublishersFor(Type messageType);

        IEnumerable<IReceiveMessages> GetReceivers();

        IEnumerable<ISendMessages> GetSendersFor(Type messageType);
    }
}