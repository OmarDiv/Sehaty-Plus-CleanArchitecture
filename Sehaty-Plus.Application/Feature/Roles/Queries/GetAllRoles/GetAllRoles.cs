using Sehaty_Plus.Application.Feature.Roles.Responses;
using Sehaty_Plus.Application.Feature.Roles.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sehaty_Plus.Application.Feature.Roles.Queries.GetAllRoles
{
    public record GetAllRoles(bool? IncludeDisabled, CancellationToken CancellationToken = default) : IRequest<Result<IEnumerable<RoleResponse>>>;
    
    public class GetAllRolesHandler(IRoleService _roleService) : IRequestHandler<GetAllRoles, Result<IEnumerable<RoleResponse>>>
    {
        public async Task<Result<IEnumerable<RoleResponse>>> Handle(GetAllRoles request, CancellationToken cancellationToken)
        {
            var roles = await _roleService.GetAllRolesAsync(request.IncludeDisabled, request.CancellationToken);
            return Result.Success(roles);
        }
    }
}
