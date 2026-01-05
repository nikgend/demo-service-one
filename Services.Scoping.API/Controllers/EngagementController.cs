using MediatR;
using Microsoft.AspNetCore.Mvc;
using Services.Scoping.Application.Queries.Engagement;
using Services.Scoping.Domain.AggregateRoots.EngagementAggregate;
using Services.Scoping.Domain.Contracts.Engagement;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Text;

namespace Services.Scoping.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EngagementController : ControllerBase
    {
        private readonly IMediator _mediator;
        private StringBuilder _validationErrMsg = new();

        public EngagementController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [Route("Get")]
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ApiResponse<List<EngagementDetails>>> GetEngagementDetails()
        {
            try
            {
                var result = await _mediator.Send(new GetEngagementDetailsQuery());
                return ApiResponse<List<EngagementDetails>>.Success(result);
            }
            catch (Exception ex)
            {
                return new ApiResponse<List<EngagementDetails>> { Succeeded = false, ErrorMessage = ex.Message };
            }
        }

        [Route("Add")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ApiResponse<string>> Add([FromBody] CreateEngagementRequest req)
        {
            try
            {

                var result = await _mediator.Send(req);

                return (result.IsNameExist ? ApiResponse<string>.DuplicateCheck(result.IsNameExist, result.message)
                    : ApiResponse<string>.Success(result.message));

            }
            catch (ValidationException validationExp)
            {
                return ApiResponse<string>.Fail(_validationErrMsg.ToString());
            }
            catch (Exception ex)
            {
                //  _logger.LogError(ex);
                return ApiResponse<string>.Fail(ex.Message);
            }
        }

        [Route("Update")]
        [HttpPut]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ApiResponse<string>> EditEngagement([FromBody] UpdateEngagementRequest req)
        {
            try
            {

                var updateData = await _mediator.Send(req);

                return (updateData.IsNameExist) ?
                    ApiResponse<string>.DuplicateCheck(updateData.IsNameExist, updateData.message)
                    : ApiResponse<string>.Success(updateData.message);

            }
            catch (Exception ex)
            {
                return ApiResponse<string>.Fail(ex.Message);
            }
        }
    }
}

