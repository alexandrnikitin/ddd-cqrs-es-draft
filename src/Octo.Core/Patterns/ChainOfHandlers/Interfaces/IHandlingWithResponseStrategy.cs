using System.Collections.Generic;

namespace Octo.Core.Patterns.ChainOfHandlers.Interfaces
{
    public interface IHandlingWithResponseStrategy<in TContext, in THandler, out TOut>
    {
        #region Public Methods and Operators

        TOut Process(IEnumerable<THandler> handlers, TContext context);

        #endregion
    }

    //public interface IHandlersProcessorWithReturnMany<in TContext, in THandler, TOut>
    //{
    //    #region Public Methods and Operators

    //    List<TOut> Process(IEnumerable<THandler> handlers, TContext context);

    //    #endregion
    //}
}