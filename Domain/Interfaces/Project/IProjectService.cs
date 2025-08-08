using Domain.Dto.ProjectCreateDtos;
using Domain.Dto.ProjectsDtos;

namespace Domain.Interfaces.ProjectCreate
{
    public interface IProjectService
    {
        Task<int> CreateProject(ProjectCreateRequestModel model);
        Task<ProjectList> GetProjects(ProjectRequest model);
        Task<ProjectResponse> GetProjectById(int id);
    }
}
