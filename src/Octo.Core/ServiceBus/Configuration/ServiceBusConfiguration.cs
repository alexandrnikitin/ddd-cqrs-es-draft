using System;
using System.Collections.Generic;
using System.Linq;

using Octo.Core.ServiceBus.Configuration.Routing;

namespace Octo.Core.ServiceBus.Configuration
{
    public class ServiceBusConfiguration
    {
        public ServiceBusConfiguration()
        {
            MessageRoutes = new List<MessageRoute>();
            Publishers = new HashSet<Type>();
            Receivers = new HashSet<Type>();
            Senders = new HashSet<Type>();
        }

        public IEnumerable<MessageRoute> MessageRoutes { get; private set; }

        public HashSet<Type> Publishers { get; set; }

        public HashSet<Type> Receivers { get; set; }

        public HashSet<Type> Senders { get; set; }

        public MessageRoute GetOrCreateMessageRouteFor(Type messageType)
        {
            var route = MessageRoutes.SingleOrDefault(m => m.Message == messageType);

            if (route == null)
            {
                var newMessageRoute = new MessageRoute(messageType);
                var list = MessageRoutes.ToList();
                list.Add(newMessageRoute);
                MessageRoutes = list;
            }

            return route;
        }
    }
}