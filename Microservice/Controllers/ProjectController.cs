using Domain.Dto.ProjectCreateDtos;
using Domain.Dto.ProjectsDtos;
using Domain.Interfaces.ProjectCreate;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace Microservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectService _projectService;
        public ProjectController(IProjectService projectService)
        {
            _projectService = projectService;
        }
        [HttpPost]
        [ProducesResponseType(typeof(int), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> CreateProject([FromBody] ProjectCreateRequestDto model)
        {
            try
            {
                var result = await _projectService.CreateProject(model);
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

        [HttpPost("list")]
        [ProducesResponseType(typeof(ProjectResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProjectListDto>> GetProjects([FromBody] ProjectRequestDto model)
        {
            var result = await _projectService.GetProjects(model);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ProjectResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProjectResponseDto>> GetProject(int id)
        {
            try
            {
                var project = await _projectService.GetProjectById(id);
                return Ok(project);
            }
            catch (InvalidOperationException)
            {
                return NotFound();
            }
            catch (Exception)
            {
                return StatusCode(500, new { message = "An unexpected error occurred." });
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<int>> UpdateProject(int id, [FromBody] ProjectUpdateRequestDto model)
        {
            if (id <= 0)
                return BadRequest("Invalid project ID");

            var status = await _projectService.UpdateProject(id, model);

            if (!status)
                return NotFound();

            return Ok(true);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<bool>> DeleteProject(int id)
        {
            if (id <= 0)
                return BadRequest("Invalid project ID");

            var status = await _projectService.DeleteProject(id);

            if (!status)
                return NotFound();

            return Ok(true);
        }
    }
}
