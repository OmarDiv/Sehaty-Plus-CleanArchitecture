using Sehaty_Plus.Application.Feature.Roles.Commands.AddRole;
using Sehaty_Plus.Application.Feature.Roles.Responses;

namespace Sehaty_Plus.Application.Feature.Roles.Services
{
    public interface IRoleService
    {
        Task<IEnumerable<RoleResponse>> GetAllRolesAsync(bool? IncludeDisabled = false, CancellationToken cancellationToken = default);
        Task<Result<RoleDetailResponse>> GetAsync(string roleId);
        Task<Result<RoleDetailResponse>> AddRoleAsync(RoleRequest request);
        Task<Result> UpdateRoleAsync(string id, RoleRequest request, CancellationToken cancellationToken);
        Task<Result> ToggleStatusAsync(string id);
    }
}
