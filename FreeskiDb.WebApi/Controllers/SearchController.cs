using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FreeskiDb.WebApi.AzureSearch;
using FreeskiDb.WebApi.Documents;
using FreeskiDb.WebApi.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace FreeskiDb.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        private readonly ISearchClient _searchClient;

        public SearchController(ISearchClient searchClient)
        {
            _searchClient = searchClient;
        }

        // GET api/ski
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Ski>>> Get([RequiredFromQuery] string query)
        {
            var searchResult = await _searchClient.Search<Ski>(query);
            if (searchResult.Results.Count <= 0)
            {
                return Ok("No search results found");;
            }

            var skis = searchResult.Results.OrderBy(x => x.Score).Select(y => y.Document);
            return Ok(skis);
        }
    }
}   