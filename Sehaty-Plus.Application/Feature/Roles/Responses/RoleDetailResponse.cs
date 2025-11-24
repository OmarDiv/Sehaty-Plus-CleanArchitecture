using System;
using System.Collections.Generic;
using System.Text;

namespace Sehaty_Plus.Application.Feature.Roles.Responses
{
    public record RoleDetailResponse(
        string Id,
        string Name,
        bool IsDeleted,
        IEnumerable<string> Permissions
        );
}
