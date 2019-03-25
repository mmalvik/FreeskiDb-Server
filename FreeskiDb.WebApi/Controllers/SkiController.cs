using System.Threading.Tasks;
using FreeskiDb.Persistence.Entities;
using FreeskiDb.Persistence.Skis.Queries.GetSkiList;
using FreeskiDb.WebApi.Repository;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FreeskiDb.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SkiController : ControllerBase
    {
        private readonly ISkiRepository _skiRepository;
        private readonly IMediator _mediator;

        public SkiController(ISkiRepository skiRepository, IMediator mediator)
        {
            _skiRepository = skiRepository;
            _mediator = mediator;
        }

        // GET api/ski
        [HttpGet]
        public async Task<ActionResult<SkiListModel>> Get()
        {
            var res = await _mediator.Send(new GetSkiListQuery());
            //var result = await _skiRepository.List();
            return Ok(res);
        }

        // GET api/ski/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Ski>> Get(string id)
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
