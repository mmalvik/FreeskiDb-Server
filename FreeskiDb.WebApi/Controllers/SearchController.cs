using System.Linq;
using System.Threading.Tasks;
using FreeskiDb.WebApi.Documents;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Search;

namespace FreeskiDb.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        private readonly ISearchIndexClient _searchIndexClient;

        public SearchController(ISearchIndexClient searchIndexClient)
        {
            _searchIndexClient = searchIndexClient;
        }

        // GET api/ski
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var searchResult = await _searchIndexClient.Documents.SearchAsync<Ski>("k2");
            var skis = searchResult.Results.OrderBy(x => x.Score).Select(y => y.Document);
            return Ok(skis);
        }
    }
}   