using Sehaty_Plus.Application.Feature.Doctors.Queries.GetAllDoctor;
using Sehaty_Plus.Application.Feature.Doctors.Queries.GetDoctorById;
using Sehaty_Plus.Application.Feature.Doctors.Responses;

namespace Sehaty_Plus.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorsController(IMediator _mediator) : ControllerBase
    {
        [HttpGet("")]
        public async Task<ActionResult<IEnumerable<AdminDoctorListResponse>>> GetAll()
        {
            var result = await _mediator.Send(new GetAllDoctor());
            return result.AsActionResult();

        }
        [HttpGet("{DoctorId:guid}")]
        public async Task<ActionResult<AdminDoctorDetailsResponse>> GetById(Guid DoctorId)
        {
            var result = await _mediator.Send(new GetDoctorById(DoctorId));
            return result.AsActionResult();

        }
    }
}
