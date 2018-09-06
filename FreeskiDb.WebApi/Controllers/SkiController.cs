using System.Threading.Tasks;
using FreeskiDb.WebApi.Documents;
using FreeskiDb.WebApi.Repository;
using Microsoft.AspNetCore.Mvc;

namespace FreeskiDb.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SkiController : ControllerBase
    {
        private readonly ISkiRepository _skiRepository;

        public SkiController(ISkiRepository skiRepository)
        {
            _skiRepository = skiRepository;
        }

        // GET api/ski
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _skiRepository.List();
            return Ok(result);
        }

        // GET api/ski/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _skiRepository.GetById(id);
            return Ok(result);
        }

        // POST api/ski
        [HttpPost]
        public async Task Post([FromBody] Ski value)
        {
            await _skiRepository.Add(value);
        }

        // PUT api/ski/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/ski/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
