using Sehaty_Plus.Application.Feature.Specializations.Queries.GetAllSpecialization;
using Sehaty_Plus.Application.Feature.Specializations.Queries.GetSepcializtionById;
using Sehaty_Plus.Application.Feature.Specializations.Responses;
using Sehaty_Plus.Application.Feature.Specializations.Command.CreateSpecialization;
using Sehaty_Plus.Application.Feature.Specializations.Command.DeleteSpecialization;
using Sehaty_Plus.Application.Feature.Specializations.Command.ToggleSpecializationActive;
using Sehaty_Plus.Application.Feature.Specializations.Command.UpdateSpecialization;
using Microsoft.AspNetCore.Authorization;

namespace Sehaty_Plus.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SpecializationsController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SpecializationResponse>>> GetAll()
        {
            var result = await _mediator.Send(new GetAllSpecializations());
            return result.AsActionResult();

        }
        [HttpGet("{id}", Name = "GetById")]
        public async Task<ActionResult<SpecializationResponse>> GetById([FromRoute] int id, CancellationToken ancellationToken)
        {
            var result = await _mediator.Send(new GetSpecializationsById(id), ancellationToken);
            return result.AsActionResult();
        }
        [HttpPost("")]
        public async Task<ActionResult<SpecializationResponse>> Create([FromBody] CreateSpecialization request, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(request, cancellationToken);
            return result.IsSuccess ? result.AsCreatedResult(nameof(GetById), new { id = result.Value.Id }) : result.AsActionResult();
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> Update([FromRoute] int id, [FromBody] UpdateSpecializationDto request, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new UpdateSpecialization(id, request.Name, request.Description), cancellationToken);
            return result.AsNoContentResult();
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete([FromRoute] int id, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new DeleteSpecialization(id), cancellationToken);
            return result.AsNoContentResult();
        }
        [HttpPatch("{id}/toggle-active")]
        public async Task<ActionResult> ToggleActivation([FromRoute] int id, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new ToggleSpecializationActive(id), cancellationToken);
            return result.AsNoContentResult();
        }

    }
}
