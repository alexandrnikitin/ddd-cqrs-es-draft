namespace Octo.Core.ServiceBus.Configuration
{
    public static class RouteConfigurerExtensions
    {
        #region Public Methods and Operators

        // public static void PublishAll(this RouteConfigurer configurer, object messageBrokerId, Assembly assemblies, string @namespace)
        // {
        // // todo 1341 add 
        // throw new NotImplementedException();
        // }

        // public static void PublishAll(this RouteConfigurer configurer, Type publisher, params Assembly[] assemblies)
        // {
        // foreach (var assembly in assemblies)
        // {
        // // todo 1341 replace IMessage with IEvent when ready
        // var messageHandlers = assembly.GetTypes().Where(type => type.IsClass && typeof(IMessage).IsAssignableFrom(type));

        // foreach (var messageHandler in messageHandlers)
        // {
        // configurer.Publish(messageHandler, publisher);
        // }
        // }
        // }
        #endregion
    }
}