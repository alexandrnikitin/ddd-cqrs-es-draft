namespace Octo.Core.ServiceBus.Configuration.Routing
{
    public interface IRouteCondition
    {
        bool IsHandlerAcceptable(IRouteConditionContext context);
    }
}