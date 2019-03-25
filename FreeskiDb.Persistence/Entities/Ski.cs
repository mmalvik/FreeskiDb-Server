namespace FreeskiDb.Persistence.Entities
{
    public class Ski
    {
        public Ski(string brand, string model)
        {
            Brand = brand;
            Model = model;
        }

        public string Brand { get; set; }
        public string Model { get; set; }
    }
}