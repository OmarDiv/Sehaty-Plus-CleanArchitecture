﻿namespace Sehaty_Plus.Application.Feature.Auth.Commands.RevokeRefreshToken;

public record RevokeRefreshToken(string Token, string RefreshToken) : IRequest<Result>;
public class RevokeRefeshTokenHandler(IAuthService _authService) : IRequestHandler<RevokeRefreshToken, Result>
{
    public async Task<Result> Handle(RevokeRefreshToken request, CancellationToken cancellationToken)
    {
        return await _authService.RevokeRefreshTokenAsync(request.Token, request.RefreshToken, cancellationToken);
    }
}
