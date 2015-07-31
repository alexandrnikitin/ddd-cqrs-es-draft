using Octo.Core.Patterns.ChainOfHandlers.HandlingStrategies;

namespace Octo.Core.Patterns.ChainOfHandlers
{
    public class ChainOfHandlersModule : Module
    {
        #region Methods

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterGeneric(typeof(AllHandlingStrategy<,>));
        }

        #endregion
    }
}