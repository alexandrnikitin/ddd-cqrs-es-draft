using Octo.Core.ServiceBus;

namespace Octo.Core.Cqrs
{
    public interface ICommandHandler<TCommand> : IMessageHandler<TCommand> where TCommand : ICommand
    {
    }
}