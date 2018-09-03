using System;

namespace FreeskiDb.WebApi.FooBar
{
    public class Foo : IFoo
    {
        public void DoStuff()
        {
            Console.WriteLine("Does stuff");
        }
    }
}