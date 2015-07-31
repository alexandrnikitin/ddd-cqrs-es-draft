using System;
using System.Collections.Generic;

using Octo.Core.ServiceBus.Common;
using Octo.Core.ServiceBus.MessageBrokers;

namespace Octo.Core.ServiceBus.Subscriptions
{
    internal interface IMessageBrokerManager
    {
        IEnumerable<MessageHandlerContainer> GetHandlers(Type messageType, Type messageBrokerType);

        IEnumerable<IPublishMessages> GetPublishersFor(Type messageType);

        IEnumerable<IReceiveMessages> GetReceivers();

        IEnumerable<ISendMessages> GetSendersFor(Type messageType);
    }
}