using FreeskiDb.WebApi.Repository;
using LightInject;

namespace FreeskiDb.WebApi
{
    public class CompositionRoot : ICompositionRoot
    {
        public void Compose(IServiceRegistry serviceRegistry)
        {
            serviceRegistry.Register<ISkiRepository, SkiRepository>(new PerContainerLifetime());
        }
    }
}