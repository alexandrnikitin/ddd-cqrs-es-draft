using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

using Octo.ServiceBus.ServiceBus.Common;
using Octo.ServiceBus.ServiceBus.Configuration;
using Octo.ServiceBus.ServiceBus.MessageBrokers;

namespace Octo.ServiceBus.ServiceBus.Subscriptions
{
    internal class MessageBrokerManager : IMessageBrokerManager
    {
        private readonly IComponentContext _context;
        private readonly ServiceBusConfiguration _serviceBusConfiguration;

        public MessageBrokerManager(
            ServiceBusConfiguration serviceBusConfiguration, 
            IComponentContext context)
        {
            _serviceBusConfiguration = serviceBusConfiguration;

            // todo remove context and wrap into factory
            _context = context;
        }

        public IEnumerable<MessageHandlerContainer> GetHandlers(Type messageType, Type messageBrokerType)
        {
            var mbRoutes =
                _serviceBusConfiguration.MessageRoutes.Where(m => m.Message == messageType)
                    .SelectMany(m => m.ReceiveFrom)
                    .Where(m => m.ReceiveFrom == messageBrokerType);

            var handlers = new List<MessageHandlerContainer>();

            foreach (var mbRoute in mbRoutes)
            {
                // todo call context factory or some thing else, to get condition message context instance
                var acceptableRoutes = mbRoute.AcceptableHandlerRoutes(null).ToArray();

                if (!acceptableRoutes.Any())
                    throw new NoNullAllowedException(
                        "Handler route collection must have at least one acceptable handler for each message");

                acceptableRoutes = acceptableRoutes.OrderBy(m => m.Handler.Name.EndsWith("Saga")).ToArray();

                foreach (var acceptableRoute in acceptableRoutes)
                {
                    var handlerInstace = (IMessageHandler)_context.Resolve(acceptableRoute.Handler);
                    var handlerMethod = acceptableRoute.Handler.GetMethod("Handle", new[] { messageType });

                    var messageHandlerContainer = new MessageHandlerContainer
                                                      {
                                                          Instance = handlerInstace, 
                                                          Invoke =
                                                              (handler, message) =>
                                                              handlerMethod.Invoke(handler, new[] { message })
                                                      };

                    handlers.Add(messageHandlerContainer);
                }
            }

            return handlers;
        }

        public IEnumerable<IPublishMessages> GetPublishersFor(Type messageType)
        {
            var routes = _serviceBusConfiguration.MessageRoutes.Where(m => m.Message == messageType);

            foreach (var route in routes)
            {
                foreach (var publisher in route.PublishTo)
                {
                    yield return (IPublishMessages)_context.Resolve(publisher);
                }
            }
        }

        public IEnumerable<IReceiveMessages> GetReceivers()
        {
            foreach (var receiver in _serviceBusConfiguration.Receivers)
            {
                yield return (IReceiveMessages)_context.Resolve(receiver);
            }
        }

        public IEnumerable<ISendMessages> GetSendersFor(Type messageType)
        {
            return Enumerable.Empty<ISendMessages>();
        }
    }
}