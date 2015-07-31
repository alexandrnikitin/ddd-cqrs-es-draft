using System;
using System.Collections.Generic;

namespace Octo.Core.ServiceBus.Subscriptions
{
    public class MessageTypeToPublisherDictionary : Dictionary<Type, HashSet<Type>>
    {
    }
}