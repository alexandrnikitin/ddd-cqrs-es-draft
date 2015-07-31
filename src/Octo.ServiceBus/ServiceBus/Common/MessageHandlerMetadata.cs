using System;
using System.ComponentModel;

namespace Octo.ServiceBus.ServiceBus.Common
{
    public class MessageHandlerMetadata
    {
        public bool IsSaga { get; set; }

        [DefaultValue(typeof(object))]
        public Type MessageType { get; set; }
    }
}