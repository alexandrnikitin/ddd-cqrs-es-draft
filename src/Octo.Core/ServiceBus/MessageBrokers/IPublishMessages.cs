using Octo.Core.ServiceBus.Common;

namespace Octo.Core.ServiceBus.MessageBrokers
{
    public interface IPublishMessages
    {
        void Publish(MessageContext messageContext);
    }
}