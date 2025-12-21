using Microsoft.AspNetCore.Authorization;

namespace Sehaty_Plus.Filters;

public class HasPermissionAttribute(string permission) : AuthorizeAttribute(permission)
{
}