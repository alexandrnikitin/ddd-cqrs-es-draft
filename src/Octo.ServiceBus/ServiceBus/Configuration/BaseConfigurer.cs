namespace Octo.ServiceBus.ServiceBus.Configuration
{
    public abstract class BaseConfigurer
    {
        protected BaseConfigurer(ContainerBuilder containerBuilder, ServiceBusConfiguration serviceBusConfiguration)
        {
            ContainerBuilder = containerBuilder;
            ServiceBusConfiguration = serviceBusConfiguration;
        }

        public ContainerBuilder ContainerBuilder { get; private set; }

        public ServiceBusConfiguration ServiceBusConfiguration { get; private set; }
    }
}