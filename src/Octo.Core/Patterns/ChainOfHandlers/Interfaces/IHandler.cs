namespace Octo.Core.Patterns.ChainOfHandlers.Interfaces
{
    public interface IHandler<in TContext, out TOut>
    {
        TOut Handle(TContext context);
    }

    public interface IHandler<in TContext>
    {
        void Handle(TContext context);
    }
}