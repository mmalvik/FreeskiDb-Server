namespace FreeskiDb.WebApi.Config
{
    public class FreeskiDbConfiguration
    {
        public string CosmosUri { get; set; }

        public string CosmosKey  { get; set; }

        public string DatabaseName { get; set; }

        public string CollectionName { get; set; }
    }
}