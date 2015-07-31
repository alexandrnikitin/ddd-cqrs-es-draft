using System.Collections.Generic;

namespace Octo.Core.Patterns.ChainOfHandlers.Interfaces
{
    public interface IHandlingStrategy<in TContext, in THandler>
    {
        #region Public Methods and Operators

        void Process(IEnumerable<THandler> handlers, TContext context);

        #endregion
    }
}