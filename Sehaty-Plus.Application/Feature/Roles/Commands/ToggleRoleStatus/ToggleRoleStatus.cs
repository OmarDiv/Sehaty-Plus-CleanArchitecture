using Sehaty_Plus.Application.Feature.Roles.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sehaty_Plus.Application.Feature.Roles.Commands.ToggleRoleStatus
{
    public  record ToggleRoleStatus(string RoleId) : IRequest<Result>;
    public class ToggleRoleStatusHandler(IRoleService _roleService) : IRequestHandler<ToggleRoleStatus, Result>
    {
        public async Task<Result> Handle(ToggleRoleStatus request, CancellationToken cancellationToken)
        {
            var result = await _roleService.ToggleStatusAsync(request.RoleId);
            return result;
        }
    }
}
