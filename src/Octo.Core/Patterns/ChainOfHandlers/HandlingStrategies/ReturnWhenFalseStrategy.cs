using System.Collections.Generic;

using Octo.Core.Patterns.ChainOfHandlers.Interfaces;

namespace Octo.Core.Patterns.ChainOfHandlers.HandlingStrategies
{
    public class ReturnWhenFalseStrategy<TContext, THandler> : IHandlingWithResponseStrategy<TContext, THandler, bool>
        where THandler : IHandler<TContext, bool>
    {
        #region Public Methods and Operators

        public bool Process(IEnumerable<THandler> handlers, TContext context)
        {
            foreach (var handler in handlers)
            {
                if (!handler.Handle(context))
                {
                    return false;
                }
            }

            return true;
        }

        #endregion
    }
}