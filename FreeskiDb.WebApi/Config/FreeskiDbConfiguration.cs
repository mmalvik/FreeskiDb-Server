using Newtonsoft.Json;

namespace FreeskiDb.WebApi.Config
{
    public class FreeskiDbConfiguration
    {
        [JsonProperty("CosmosUri")]
        public string CosmosUri { get; set; }

        [JsonProperty("CosmosKey")]
        public string CosmosKey  { get; set; }
    }
}