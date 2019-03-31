using FreeskiDb.Persistence.Entities;

namespace Test.FreeskiDb.WebApi
{
    internal static class SkiFactory
    {
        internal static Ski K2Hellbent = new Ski
        {
            Brand = "K2",
            Model = "Hellbent",
            TipWidth = 150,
            WaistWidth = 120,
            TailWidth = 140,
            Weight = 2000
        };
    }
}