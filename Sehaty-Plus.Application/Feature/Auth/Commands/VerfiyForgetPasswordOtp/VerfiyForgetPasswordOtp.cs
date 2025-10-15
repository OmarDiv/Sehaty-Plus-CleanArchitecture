using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sehaty_Plus.Application.Feature.Auth.Commands.VerfiyForgetPasswordOtp
{
    public record VerfiyForgetPasswordOtp(string PhoneNumber,string OtpCode) : IRequest<Result<string>>;
    public class VerfiyForgetPasswordOtpHandler(IAuthService _authService) : IRequestHandler<VerfiyForgetPasswordOtp, Result<string>>
    {
        public async Task<Result<string>> Handle(VerfiyForgetPasswordOtp request, CancellationToken cancellationToken)
        {
            return await _authService.VerfiyForgetPasswordOtp(request, cancellationToken);
        }
    }
}
