using Domain.Dto.ProjectTasks;
using Domain.Interfaces.ProjectTasks;
using Microsoft.AspNetCore.Mvc;

namespace Microservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectTasksController : ControllerBase
    {
        private readonly IProjectTasksService _projectTasksService;
        public ProjectTasksController(IProjectTasksService projectTasksService)
        {
            _projectTasksService = projectTasksService;
        }

        [HttpPost]
        public async Task<ActionResult> ProjectAssign([FromBody] ProjectAssignRequestDto model)
        {
            try
            {
                int result = await _projectTasksService.ProjectAssign(model);
                return Ok(result);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { message = ex.Message });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception)
            {
                return StatusCode(500, new { message = "An unexpected error occurred." });
            }
        }
    }
}
