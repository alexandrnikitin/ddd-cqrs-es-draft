using System.Collections.Generic;

using Octo.Core.Patterns.ChainOfHandlers.Interfaces;

namespace Octo.Core.Patterns.ChainOfHandlers
{
    public abstract class ChainOfHandlersWithResponse<TContext, THandler, TReturn>
        where THandler : IHandler<TContext, TReturn>
    {
        #region Fields

        private readonly IEnumerable<THandler> _handlers;

        private readonly IHandlingWithResponseStrategy<TContext, THandler, TReturn> _handlingStrategy;

        #endregion

        #region Constructors and Destructors

        protected ChainOfHandlersWithResponse(
            IHandlingWithResponseStrategy<TContext, THandler, TReturn> handlingStrategy, 
            IEnumerable<THandler> handlers)
        {
            _handlingStrategy = handlingStrategy;
            _handlers = handlers;
        }

        #endregion

        #region Methods

        protected TReturn Process(TContext context)
        {
            return _handlingStrategy.Process(_handlers, context);
        }

        #endregion
    }
}