using System.Collections.Generic;

using Octo.Core.Patterns.ChainOfHandlers.Interfaces;

namespace Octo.Core.Patterns.ChainOfHandlers.HandlingStrategies
{
    public class AllHandlingStrategy<TContext, THandler> : IHandlingStrategy<TContext, THandler>
        where THandler : IHandler<TContext>
    {
        #region Public Methods and Operators

        public void Process(IEnumerable<THandler> handlers, TContext context)
        {
            foreach (var handler in handlers)
            {
                handler.Handle(context);
            }
        }

        #endregion
    }
}