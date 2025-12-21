using Microsoft.AspNetCore.Authorization;

namespace Sehaty_Plus.Filters;

public class PermissionRequirement(string permission) : IAuthorizationRequirement
{
    public string Permission { get; } = permission;
}