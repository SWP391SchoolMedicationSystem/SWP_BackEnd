using BussinessLayer.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SchoolMedicalSystem.Controllers
{
    [Route("api")]
    [ApiController]
    public class HealthcheckrecordeventController(IHealthCheckEventRecordService) : ControllerBase
    {
        [HttpGet]
        [Route("healthcheckrecordevents")]
    }
}
