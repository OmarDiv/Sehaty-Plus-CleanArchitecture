using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Sehaty_Plus.Application.Common.Authentication
{
    public class JwtProvider(IOptions<JwtOptions> options) : IJwtProvider
    {
        private readonly JwtOptions _options = options.Value;

        public (string token, DateTime expiresIn) GenerateToken(ApplicationUser user)
        {
            Claim[] claims = [
                new( JwtRegisteredClaimNames.Sub, user.Id),
                new( JwtRegisteredClaimNames.Email, user.Email!),
                new( JwtRegisteredClaimNames.GivenName, user.FirstName),
                new( JwtRegisteredClaimNames.FamilyName, user.LastName),
                new( JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())
                ];

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.Key));
            var singningCreadeintial = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var expiresIn = DateTime.UtcNow.AddHours(_options.ExpirationInMinutes);

            var token = new JwtSecurityToken(
                issuer: _options.Issuer,
               audience: _options.Audience,
                claims: claims,
                expires: expiresIn,
                signingCredentials: singningCreadeintial

               );
            return (token: new JwtSecurityTokenHandler().WriteToken(token), expiresIn);

        }

        public string? ValidateToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var symmeticSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.Key));
            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = symmeticSecurityKey,
                    ClockSkew = TimeSpan.Zero,
                }, out SecurityToken securityToken);
                var jwtToken = (JwtSecurityToken)securityToken;
                return jwtToken.Claims.First(x => x.Type == JwtRegisteredClaimNames.Sub).Value;
            }
            catch
            {
                return null;
            }
        }
    }
}
