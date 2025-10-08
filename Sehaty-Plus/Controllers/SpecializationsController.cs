using Microsoft.AspNetCore.Authorization;
using Sehaty_Plus.Application.Feature.Specializations.Command.CreateSpecialization;
using Sehaty_Plus.Application.Feature.Specializations.Command.DeleteSpecialization;
using Sehaty_Plus.Application.Feature.Specializations.Command.ToggleSpecializationActive;
using Sehaty_Plus.Application.Feature.Specializations.Command.UpdateSpecialization;
using Sehaty_Plus.Application.Feature.Specializations.Queries.GetAllSpecialization;
using Sehaty_Plus.Application.Feature.Specializations.Queries.GetAllSpecializationsDetailed;
using Sehaty_Plus.Application.Feature.Specializations.Queries.GetSepcializtionById;
using Sehaty_Plus.Application.Feature.Specializations.Queries.GetSpecializationByIdDetailed;
using Sehaty_Plus.Application.Feature.Specializations.Responses;

namespace Sehaty_Plus.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SpecializationsController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;


        [HttpGet]
        //[Authorize(Roles = "User,Admin")]
        public async Task<ActionResult<IEnumerable<SpecializationResponse>>> GetAll()
        {
            var result = await _mediator.Send(new GetAllSpecializations());
            return result.AsActionResult();
        }

        [HttpGet("{id}")]
        //[Authorize(Roles = "User,Admin")]
        public async Task<ActionResult<SpecializationResponse>> GetById([FromRoute] int id)
        {
            var result = await _mediator.Send(new GetSpecializationById(id));
            return result.AsActionResult();
        }

        [HttpGet("admin")]
        // [Authorize(Roles = "Admin")]
        public async Task<ActionResult<IEnumerable<SpecializationDetailedResponse>>> GetAllDetailed()
        {
            var result = await _mediator.Send(new GetAllSpecializationsDetailed());
            return result.AsActionResult();
        }

        [HttpGet("admin/{id}")]
        // [Authorize(Roles = "Admin")]
        public async Task<ActionResult<SpecializationDetailedResponse>> GetByIdDetailed([FromRoute] int id)
        {
            var result = await _mediator.Send(new GetSpecializationByIdDetailed(id));
            return result.AsActionResult();
        }

        [HttpPost]
        //  [Authorize(Roles = "Admin")]
        public async Task<ActionResult<SpecializationDetailedResponse>> Create([FromBody] CreateSpecialization request)
        {
            var result = await _mediator.Send(request);
            return result.IsSuccess
                ? result.AsCreatedResult(nameof(GetByIdDetailed), new { id = result.Value.Id })
                : result.AsActionResult();
        }

        [HttpPut("{id}")]
        // [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Update([FromRoute] int id, [FromBody] UpdateSpecializationDto request)
        {
            var result = await _mediator.Send(new UpdateSpecialization(id, request.Name, request.Description));
            return result.AsNoContentResult();
        }

        [HttpDelete("{id}")]
        // [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Delete([FromRoute] int id)
        {
            var result = await _mediator.Send(new DeleteSpecialization(id));
            return result.AsNoContentResult();
        }

        [HttpPatch("{id}/toggle-active")]
        // [Authorize(Roles = "Admin")]
        public async Task<ActionResult> ToggleActivation([FromRoute] int id)
        {
            var result = await _mediator.Send(new ToggleSpecializationActive(id));
            return result.AsNoContentResult();
        }
    }
}
