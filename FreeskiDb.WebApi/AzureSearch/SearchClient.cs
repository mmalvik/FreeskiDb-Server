using System.Threading.Tasks;
using Microsoft.Azure.Search;
using Microsoft.Azure.Search.Models;

namespace FreeskiDb.WebApi.AzureSearch
{
    public class SearchClient : ISearchClient
    {
        private readonly SearchIndexClient _client;

        public SearchClient(string searchServiceName, string indexName, string apiKey)
        {
            _client = new SearchIndexClient(searchServiceName, indexName, new SearchCredentials(apiKey));
        }


        public async Task<DocumentSearchResult<T>> Search<T>(string searchText) where T : class
        {
            return await _client.Documents.SearchAsync<T>(searchText);
        }
    }
}