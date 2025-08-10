using Domain.Dto.ProjectTasks;
using Microsoft.AspNetCore.Mvc;

namespace Microservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectTasksController : ControllerBase
    {
        public ProjectTasksController() { }

        [HttpPost]
        public async Task<ActionResult> ProjectAssign([FromBody] ProjectAssignRequestDto model)
        {
            return Ok();
        }
    }
}
