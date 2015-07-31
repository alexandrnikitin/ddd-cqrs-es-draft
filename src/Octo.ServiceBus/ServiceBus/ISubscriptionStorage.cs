using System;
using System.Collections.Generic;

namespace Octo.ServiceBus.ServiceBus
{
    internal interface ISubscriptionStorage
    {
        void Add(Type messageType, Type messageHandlerType);

        IEnumerable<Type> GetFor(Type messageType);
    }
}