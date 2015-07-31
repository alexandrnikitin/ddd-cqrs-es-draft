using System;

namespace Octo.ServiceBus.ServiceBus
{
    public interface IMessageHandlerContainer
    {
        object Instance { get; set; }

        Action<object, object> Invoke { get; set; }
    }
}