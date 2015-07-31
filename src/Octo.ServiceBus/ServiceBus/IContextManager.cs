using Octo.ServiceBus.ServiceBus.Common;

namespace Octo.ServiceBus.ServiceBus
{
    public interface IContextManager
    {
        Context Current { get; }

        void Pop();

        void Push(Context context);
    }
}