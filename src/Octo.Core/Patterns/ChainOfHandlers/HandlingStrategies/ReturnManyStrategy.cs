using System.Collections.Generic;

using Octo.Core.Patterns.ChainOfHandlers.Interfaces;

namespace Octo.Core.Patterns.ChainOfHandlers.HandlingStrategies
{
    public class ReturnManyStrategy<TContext, THandler, TOut> :
        IHandlingWithResponseStrategy<TContext, THandler, IEnumerable<TOut>>
        where THandler : IHandler<TContext, IEnumerable<TOut>>
    {
        #region Public Methods and Operators

        public IEnumerable<TOut> Process(IEnumerable<THandler> handlers, TContext context)
        {
            var list = new List<TOut>();
            foreach (var handler in handlers)
            {
                var result = handler.Handle(context);
                if (result != null)
                {
                    list.AddRange(result);
                }
            }
            return list;
        }

        #endregion
    }
}