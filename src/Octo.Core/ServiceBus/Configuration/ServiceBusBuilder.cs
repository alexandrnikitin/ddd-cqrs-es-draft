using System;
using System.Collections.Generic;

using Autofac;

using TMH.DDD.ServiceBus.Subscriptions;

namespace TMH.DDD.ServiceBus.Configure
{
    public class ServiceBusBuilder
    {
        #region Fields

        private readonly ContainerBuilder _containerBuilder;

        private readonly MessageReceiversHashSet _messageReceivers = new MessageReceiversHashSet();

        private readonly MessageTypeToMessageBrokerDictionary _messageTypeToMessageBrokerDictionaryLookup = new MessageTypeToMessageBrokerDictionary();

        #endregion

        #region Constructors and Destructors

        public ServiceBusBuilder(ContainerBuilder containerBuilder)
        {
            _containerBuilder = containerBuilder;
        }

        #endregion

        #region Public Methods and Operators

        public void Build()
        {
            _containerBuilder.RegisterInstance(_messageTypeToMessageBrokerDictionaryLookup);
            _containerBuilder.RegisterInstance(_messageReceivers);
        }

        public void ReceiveFrom(object messageBrokerId)
        {
            _messageReceivers.Add(messageBrokerId);
        }

        public void SendTo(Type messageType, object messageBrokerId)
        {
            HashSet<object> messageBrokers;
            if (_messageTypeToMessageBrokerDictionaryLookup.TryGetValue(messageType, out messageBrokers))
            {
                messageBrokers.Add(messageBrokerId);
                return;
            }

            _messageTypeToMessageBrokerDictionaryLookup.Add(messageType, new HashSet<object>(new[] { messageBrokerId }));
        }

        public void SendTo<TMessage>(object messageBrokerId) where TMessage : IMessage
        {
            SendTo(typeof(TMessage), messageBrokerId);
        }

        #endregion
    }
}