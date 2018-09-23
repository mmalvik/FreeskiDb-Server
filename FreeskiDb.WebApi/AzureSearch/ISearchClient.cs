using System.Threading.Tasks;
using Microsoft.Azure.Search.Models;

namespace FreeskiDb.WebApi.AzureSearch
{
    public interface ISearchClient
    {
        Task<DocumentSearchResult<T>> Search<T>(string searchText) where T : class;
    }
}