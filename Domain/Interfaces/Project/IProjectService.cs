using Domain.Dto.ProjectCreateDtos;
using Domain.Dto.ProjectsDtos;

namespace Domain.Interfaces.ProjectCreate
{
    public interface IProjectService
    {
        Task<int> CreateProject(ProjectCreateRequestDto model);
        Task<ProjectListDto> GetProjects(ProjectRequestDto model);
        Task<ProjectResponseDto> GetProjectById(int id);
        Task<bool> DeleteProject(int id);
        Task<bool> UpdateProject(int id, ProjectUpdateRequestDto model);
    }
}
