using Octo.ServiceBus.ServiceBus.Common;

namespace Octo.ServiceBus.ServiceBus.MessageBrokers
{
    public interface IPublishMessages
    {
        void Publish(MessageContext messageContext);
    }
}