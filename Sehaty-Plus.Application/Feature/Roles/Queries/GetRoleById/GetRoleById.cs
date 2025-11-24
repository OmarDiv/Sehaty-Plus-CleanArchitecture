using Sehaty_Plus.Application.Feature.Roles.Responses;
using Sehaty_Plus.Application.Feature.Roles.Services;

namespace Sehaty_Plus.Application.Feature.Roles.Queries.GetRoleById
{
    public record GetRoleById(string Id) : IRequest<Result<RoleDetailResponse>>;

    public class GetRoleByIdHandler(IRoleService _roleService) : IRequestHandler<GetRoleById, Result<RoleDetailResponse>>
    {
        public async Task<Result<RoleDetailResponse>> Handle(GetRoleById request, CancellationToken cancellationToken)
        {
            return await _roleService.GetAsync(request.Id);
        }
    }
}
