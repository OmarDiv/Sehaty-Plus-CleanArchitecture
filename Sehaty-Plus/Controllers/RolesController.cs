using Sehaty_Plus.Application.Common.Authentication.Filters;
using Sehaty_Plus.Application.Feature.Roles.Commands.AddRole;
using Sehaty_Plus.Application.Feature.Roles.Commands.ToggleRoleStatus;
using Sehaty_Plus.Application.Feature.Roles.Commands.UpdateRole;
using Sehaty_Plus.Application.Feature.Roles.Queries.GetAllRoles;
using Sehaty_Plus.Application.Feature.Roles.Queries.GetRoleById;
using Sehaty_Plus.Application.Feature.Roles.Responses;

namespace Sehaty_Plus.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController(IMediator _mediator) : ControllerBase
    {
        [HttpGet("")]
        [HasPermission(Permissions.GetRoles)]
        public async Task<ActionResult<IEnumerable<RoleResponse>>> GetAll([FromQuery] bool IncludeDisabled, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetAllRoles(IncludeDisabled), cancellationToken);
            return result.AsActionResult();
        }

        [HttpGet("{id}", Name = nameof(GetById))]
        [HasPermission(Permissions.GetRoles)]
        public async Task<ActionResult<RoleDetailResponse>> GetById([FromRoute]string id, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetRoleById(id), cancellationToken);

            return result.AsActionResult();
        }

        [HttpPost("")]
        [HasPermission(Permissions.AddRole)]
        public async Task<ActionResult<RoleDetailResponse>> Add([FromBody] RoleRequest request)
        {
            // Assuming you have a method to create a role
            var result = await _mediator.Send(new AddRole(request));

            return result.IsSuccess
                ? result.AsCreatedResult(nameof(GetById), new { id = result.Value.Id })
                : result.AsActionResult();

        }

        [HttpPut("{id}")]
        [HasPermission(Permissions.UpdateRole)]
        public async Task<ActionResult<Result>> Update(string id, [FromBody] RoleRequest request, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new UpdateRole(id, request), cancellationToken);

            return result.AsNoContentResult();

        }
        [HttpPut("{id}/toggle-status")]
        [HasPermission(Permissions.UpdateRole)]

        public async Task<ActionResult<Result>> ToggleStatus(string id, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new ToggleRoleStatus(id), cancellationToken);

            return result.AsNoContentResult();

        }

    }
}
