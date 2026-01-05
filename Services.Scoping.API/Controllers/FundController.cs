using MediatR;
using Microsoft.AspNetCore.Mvc;
using Services.Scoping.Application.Queries;
using Services.Scoping.Domain.AggregateRoots.FundAggregate;
using Services.Scoping.Domain.Contracts;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Text;

namespace Services.Scoping.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FundController : ControllerBase
    {
        private readonly IMediator _mediator;

        public FundController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }
        [Route("allFunds/{engagementId}")]
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ApiResponse<List<AssetManagementAnalysisDetails>>> GetAllFunds(int engagementId)
        {

            try
            {
                var result = await _mediator.Send(new GetAllFundsQuery(engagementId));

                return ApiResponse<List<AssetManagementAnalysisDetails>>.Success(result);
            }
            catch (Exception ex)
            {
                return ApiResponse<List<AssetManagementAnalysisDetails>>.Fail(ex.Message);
            }
        }

        [Route("addFund")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ApiResponse<AddFundResponse>> Add([FromBody] CreateFundRequest req)
        {

            try
            {
                string fundAdded = string.Empty;
                int res = await _mediator.Send(req);

                if (res > 0)
                {
                    AddFundResponse response = new AddFundResponse()
                    {
                        FundId = res,
                        message = $"{res} has been added successfully: {req.Name.Trim()}."
                    };

                    return ApiResponse<AddFundResponse>.Success(response);
                }
                else
                {
                    return ApiResponse<AddFundResponse>.Fail($"Failed to add {req.Name.Trim()}.");
                }
            }
            catch (Exception ex)
            {
                // _logger.LogError(ex);
                return ApiResponse<AddFundResponse>.Fail(ex.Message);
            }
        }
    }
}
