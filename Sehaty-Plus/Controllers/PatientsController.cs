using Microsoft.AspNetCore.Authorization;
using Sehaty_Plus.Application.Feature.Patients.Queries.GetAllPatients;
using Sehaty_Plus.Application.Feature.Patients.Queries.GetPatientById;
using Sehaty_Plus.Application.Feature.Patients.Queries.GetPatientProfile;
using Sehaty_Plus.Application.Feature.Patients.Responses;

namespace Sehaty_Plus.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize()]
    public class PatientsController(IMediator _mediator) : ControllerBase
    {
        [HttpGet("info")]
        public async Task<ActionResult<PatientProfileResponse>> GetFullProfile()
        {
            var result = await _mediator.Send(new GetPatientProfile(User.GetUserId()!));
            return result.AsActionResult();
        }
        [HttpGet("")]
        public async Task<ActionResult<IEnumerable<AdminPatientListResponse>>> GetAll()
        {
            var result = await _mediator.Send(new GetAllPatients());
            return result.AsActionResult();
        }
        [HttpGet("{patientId}")]
        public async Task<ActionResult<AdminPatientDetailsResponse>> GetById(string patientId)
        {
            var result = await _mediator.Send(new GetPatientById(patientId));
            return result.AsActionResult();
        }


    }
}
