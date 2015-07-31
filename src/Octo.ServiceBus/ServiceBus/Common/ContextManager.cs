using System.Collections.Concurrent;

namespace Octo.ServiceBus.ServiceBus.Common
{
    internal class ContextManager : IContextManager
    {
        private readonly ConcurrentStack<Context> _contextStack = new ConcurrentStack<Context>();

        public Context Current
        {
            get
            {
                Context context;
                if (_contextStack.TryPeek(out context))
                {
                    return context;
                }

                context = new RootContext();
                _contextStack.Push(context);
                return context;
            }
        }

        public void Pop()
        {
            Context result;
            _contextStack.TryPop(out result);
        }

        public void Push(Context context)
        {
            _contextStack.Push(context);
        }
    }
}