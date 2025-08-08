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
        public async Task<ActionResult<int>> CreateProject([FromBody] ProjectCreateRequestModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var projectId = await _projectService.CreateProject(model);

            return CreatedAtAction(nameof(GetProject), new { id = projectId }, projectId);
        }

        [HttpPost("list")]
        [ProducesResponseType(typeof(ProjectResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProjectList>> GetProjects([FromBody] ProjectRequest model)
        {
            var result = await _projectService.GetProjects(model);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ProjectResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProjectResponse>> GetProject(int id)
        {
            var project = await _projectService.GetProjectById(id);
            if (project == null)
            {
                return NotFound();
            }

            return Ok(project);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<int>> UpdateProject(int id, [FromBody] ProjectUpdateRequest model)
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
