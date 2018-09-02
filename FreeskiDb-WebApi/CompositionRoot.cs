using FreeskiDb_WebApi.FooBar;
using LightInject;

namespace FreeskiDb_WebApi
{
    public class CompositionRoot : ICompositionRoot
    {
        public void Compose(IServiceRegistry serviceRegistry)
        {
            serviceRegistry.Register<IFoo, Foo>(new PerRequestLifeTime());
        }
    }
}