using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FreeskiDb.Persistence.Entities;
using FreeskiDb.Persistence.Skis.Commands.CreateSki;
using FreeskiDb.Persistence.Skis.Commands.DeleteSki;
using FreeskiDb.Persistence.Skis.Queries.GetSkiDetails;
using FreeskiDb.Persistence.Skis.Queries.GetSkiList;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FreeskiDb.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SkiController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SkiController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET api/ski
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SkiDocument>>> Get()
        {
            var result = await _mediator.Send(new GetSkiListQuery());
            return Ok(result);
        }

        // GET api/ski/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SkiDocument>> Get(string id)
        {
            var guid = Guid.Parse(id);
            var result = await _mediator.Send(new GetSkiDetailsQuery(guid));
            return Ok(result);
        }

        // POST api/ski
        [HttpPost]
        public async Task<ActionResult<Ski>> Post([FromBody] Ski value)
        {
            var result = await _mediator.Send(new CreateSkiCommand {Ski = value});
            return CreatedAtAction($"{nameof(Get)}", result);
        }

        // PUT api/ski/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/ski/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _mediator.Send(new DeleteSkiCommand {Id = id});
            return NoContent();
        }
    }
}
