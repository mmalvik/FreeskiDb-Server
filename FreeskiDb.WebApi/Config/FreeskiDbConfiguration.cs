using Newtonsoft.Json;

namespace FreeskiDb.WebApi.Config
{
    public class FreeskiDbConfiguration
    {
        [JsonProperty("CosmosUri")]
        public string CosmosUri { get; set; }

        [JsonProperty("CosmosKey")]
        public string CosmosKey  { get; set; }

        [JsonProperty("AzureSearchUri")]
        public string AzureSearchUri { get; set; }

        [JsonProperty("AzureSearchKey")]
        public string AzureSearchKey { get; set; }

        [JsonProperty("AzureSearchServiceName")]
        public string AzureSearchServiceName { get; set; }
    }
}