using FreeskiDb.WebApi.FooBar;
using LightInject;

namespace FreeskiDb.WebApi
{
    public class CompositionRoot : ICompositionRoot
    {
        public void Compose(IServiceRegistry serviceRegistry)
        {
            serviceRegistry.Register<IFoo, Foo>(new PerRequestLifeTime());
        }
    }
}