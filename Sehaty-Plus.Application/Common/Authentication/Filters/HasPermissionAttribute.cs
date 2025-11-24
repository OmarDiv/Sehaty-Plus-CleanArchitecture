using Microsoft.AspNetCore.Authorization;

namespace Sehaty_Plus.Application.Common.Authentication.Filters;

public class HasPermissionAttribute(string permission) : AuthorizeAttribute(permission)
{
}