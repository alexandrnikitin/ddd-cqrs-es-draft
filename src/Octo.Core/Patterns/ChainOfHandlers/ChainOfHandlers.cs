using System.Collections.Generic;

using Octo.Core.Patterns.ChainOfHandlers.Interfaces;

namespace Octo.Core.Patterns.ChainOfHandlers
{
    public abstract class ChainOfHandlers<TContext, THandler>
        where THandler : IHandler<TContext>
    {
        #region Fields

        private readonly IEnumerable<THandler> _handlers;

        private readonly IHandlingStrategy<TContext, THandler> _handlingStrategy;

        #endregion

        #region Constructors and Destructors

        protected ChainOfHandlers(
            IHandlingStrategy<TContext, THandler> handlingStrategy, 
            IEnumerable<THandler> handlers)
        {
            _handlingStrategy = handlingStrategy;
            _handlers = handlers;
        }

        #endregion

        #region Methods

        protected void Process(TContext context)
        {
            _handlingStrategy.Process(_handlers, context);
        }

        #endregion
    }
}