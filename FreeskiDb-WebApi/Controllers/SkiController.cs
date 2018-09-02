using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace FreeskiDb_WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SkiController : ControllerBase
    {
        // GET api/ski
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "Ski 1", "Ski 2" };
        }

        // GET api/ski/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "Ski 1";
        }

        // POST api/ski
        [HttpPost]
        public void Post([FromBody] string value)
        {
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
