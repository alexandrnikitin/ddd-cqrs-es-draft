using Octo.Core.ServiceBus.Common;

namespace Octo.Core.ServiceBus
{
    public interface IContextManager
    {
        Context Current { get; }

        void Pop();

        void Push(Context context);
    }
}