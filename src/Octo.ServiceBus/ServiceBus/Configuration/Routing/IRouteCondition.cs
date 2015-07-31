namespace Octo.ServiceBus.ServiceBus.Configuration.Routing
{
    public interface IRouteCondition
    {
        bool IsHandlerAcceptable(IRouteConditionContext context);
    }
}