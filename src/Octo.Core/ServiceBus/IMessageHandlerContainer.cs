using System;

namespace Octo.Core.ServiceBus
{
    public interface IMessageHandlerContainer
    {
        object Instance { get; set; }

        Action<object, object> Invoke { get; set; }
    }
}