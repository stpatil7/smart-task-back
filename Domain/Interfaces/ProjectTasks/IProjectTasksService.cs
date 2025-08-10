using Domain.Dto.ProjectTasks;

namespace Domain.Interfaces.ProjectTasks
{
    public interface IProjectTasksService
    {
        Task<int> ProjectAssign(ProjectAssignRequestDto model);
    }
}
