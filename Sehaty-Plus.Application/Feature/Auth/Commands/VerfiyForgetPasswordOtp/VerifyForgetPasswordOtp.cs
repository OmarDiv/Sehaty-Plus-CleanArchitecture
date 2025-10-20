using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sehaty_Plus.Application.Feature.Auth.Commands.VerfiyForgetPasswordOtp
{
    public record VerifyForgetPasswordOtp(string PhoneNumber,string OtpCode) : IRequest<Result<string>>;
    public class VerifyForgetPasswordOtpHandler(IAuthService _authService) : IRequestHandler<VerifyForgetPasswordOtp, Result<string>>
    {
        public async Task<Result<string>> Handle(VerifyForgetPasswordOtp request, CancellationToken cancellationToken)
        {
            return await _authService.VerfiyForgetPasswordOtp(request, cancellationToken);
        }
    }
}
