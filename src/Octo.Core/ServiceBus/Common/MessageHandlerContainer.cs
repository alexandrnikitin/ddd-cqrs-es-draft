using System;

namespace Octo.Core.ServiceBus.Common
{
    internal class MessageHandlerContainer : IMessageHandlerContainer
    {
        public object Instance { get; set; }

        public Action<object, object> Invoke { get; set; }
    }
}