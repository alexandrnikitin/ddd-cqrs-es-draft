using System.Collections.Generic;

namespace Octo.Core.ServiceBus.Common
{
    public abstract class Context
    {
        protected readonly Context ParentContext;

        private readonly Dictionary<string, object> _storage = new Dictionary<string, object>();

        protected Context(Context parentContext)
        {
            ParentContext = parentContext;
        }

        public T Get<T>()
        {
            return Get<T>(typeof(T).FullName);
        }

        public T Get<T>(string key)
        {
            T result;

            if (!TryGet(key, out result))
            {
                throw new KeyNotFoundException("No item found in context with key: " + key);
            }

            return result;
        }

        public void Set<T>(T t)
        {
            Set(typeof(T).FullName, t);
        }

        public void Set<T>(string key, T t)
        {
            _storage[key] = t;
        }

        public bool TryGet<T>(out T result)
        {
            return TryGet(typeof(T).FullName, out result);
        }

        public bool TryGet<T>(string key, out T result)
        {
            object value;
            if (_storage.TryGetValue(key, out value))
            {
                result = (T)value;
                return true;
            }

            if (ParentContext != null)
            {
                return ParentContext.TryGet(key, out result);
            }

            result = default(T);
            return false;
        }
    }
}