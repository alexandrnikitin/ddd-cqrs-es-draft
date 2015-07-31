using System;
using System.ComponentModel;

namespace Octo.Core.ServiceBus.Common
{
    public class MessageHandlerMetadata
    {
        public bool IsSaga { get; set; }

        [DefaultValue(typeof(object))]
        public Type MessageType { get; set; }
    }
}