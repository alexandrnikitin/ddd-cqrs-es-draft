namespace Octo.ServiceBus.ServiceBus.Common
{
    public class MessageContext : Context
    {
        public MessageContext(Context parentContext, IMessageEnvelop messageEnvelop)
            : base(parentContext)
        {
            Set(messageEnvelop);
        }

        protected MessageContext(Context parentContext)
            : base(parentContext)
        {
        }

        public IMessageEnvelop MessageEnvelop
        {
            get { return Get<IMessageEnvelop>(); }
            set { Set(value); }
        }
    }
}