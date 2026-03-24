using MediatR;
using Microsoft.AspNetCore.Mvc;
using TEST.AMRCloud.Services.Scoping.Application.DbOps;
using TEST.AMRCloud.Services.Scoping.Domain.Contracts;

namespace TEST.AMRCloud.Services.Scoping.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FundController : ControllerBase
    {
        private readonly IMediator _mediator;

        public FundController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<FundDto>> GetFundById(int id)
        {
            var fund = await _mediator.Send(new GetFundByIdQuery(id));
            return Ok(fund);
        }

        [HttpGet]
        public async Task<ActionResult<List<FundDto>>> GetAllFunds()
        {
            var funds = await _mediator.Send(new GetAllFundsQuery());
            return Ok(funds);
        }

        [HttpPost]
        public async Task<ActionResult<FundDto>> CreateFund([FromBody] CreateFundCommand command)
        {
            var fund = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetFundById), new { id = fund.Id }, fund);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<FundDto>> UpdateFund(int id, [FromBody] UpdateFundCommand command)
        {
            if (id != command.Id)
                return BadRequest("Id mismatch.");

            var fund = await _mediator.Send(command);
            return Ok(fund);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteFund(int id)
        {
            var result = await _mediator.Send(new DeleteFundCommand { Id = id });
            if (!result)
                return NotFound();

            return NoContent();
        }

    }
}
