using Sehaty_Plus.Application.Common.Authentication;
using Sehaty_Plus.Application.Common.Types;
using Sehaty_Plus.Application.Feature.Clinc.Commands.AddClinc;

namespace Sehaty_Plus.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClincsController(IMediator mediator) : ControllerBase
    {

        [HttpPost]
        [HasPermission(Permissions.AddClinic)]
        public async Task<ActionResult<Result>> AddClinc([FromBody] AddClinicDto addClinicDto)
        {
            var result = await mediator.Send(new AddClinc(User.GetUserId()!, addClinicDto));
            return result.AsNoContentResult();
        }
    }
}
