using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FreeskiDb.Persistence.Entities;
using FreeskiDb.Persistence.Skis.Commands.CreateSki;
using FreeskiDb.Persistence.Skis.Commands.DeleteSki;
using FreeskiDb.Persistence.Skis.Commands.UpdateSki;
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
        [ProducesResponseType(typeof(IEnumerable<SkiDocument>), 200)]
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
            // TODO: Return string here instead?
            var guidId = await _mediator.Send(new CreateSkiCommand {Ski = value});
            var result = guidId.ToString();
            return CreatedAtAction($"{nameof(Get)}", result);
        }

        // TODO: Handle multiple scenarios
        // If a new resource is created, the origin server MUST inform the user agent via the 201 (Created) response.
        // If an existing resource is modified, either the 200 (OK) or 204 (No Content) response codes SHOULD be
        // sent to indicate successful completion of the request.
        // https://www.w3.org/Protocols/rfc2616/rfc2616-sec9.html#sec9.6

        // PUT api/ski/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, [FromBody] Ski value)
        {
            await _mediator.Send(new UpdateSkiCommand {Id = Guid.Parse(id), Ski = value});
            return NoContent();
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
