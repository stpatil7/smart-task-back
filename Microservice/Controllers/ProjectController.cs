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

        [HttpPost("/")]
        public async Task<ActionResult<ProjectList>> GetProjects([FromBody] ProjectRequest model)
        {
            var result = await _projectService.GetProjects(model);
            return result;
        }

        [HttpPost("/CreateProject")]
        public async Task<ActionResult<int>> CreateProject([FromBody] ProjectCreateRequestModel model)
        {
            int projectId = await _projectService.CreateProject(model);
            return projectId;
        }

    }
}
