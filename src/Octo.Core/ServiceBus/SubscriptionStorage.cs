using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace Octo.Core.ServiceBus
{
    internal class SubscriptionStorage : ISubscriptionStorage
    {
        private readonly ConcurrentDictionary<Type, List<Type>> _subscriptions =
            new ConcurrentDictionary<Type, List<Type>>();

        public void Add(Type messageType, Type messageHandlerType)
        {
            _subscriptions.AddOrUpdate(
                messageType, 
                type => new List<Type> { type }, 
                (type, list) => new List<Type>(list) { type });
        }

        public IEnumerable<Type> GetFor(Type messageType)
        {
            List<Type> list;

            return _subscriptions.TryGetValue(messageType, out list) ? list : Enumerable.Empty<Type>();
        }
    }
}