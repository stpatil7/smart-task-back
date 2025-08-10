using DataAccessLayer.Entities;
using Domain.Dto.ProjectTasks;
using Domain.Interfaces.ProjectTasks;
using Domain.Interfaces.Repository;

namespace Application.Services
{
    public class ProjectTasksService : IProjectTasksService
    {
        private readonly IRepository<ProjectTasks> _projectTasksRepository;
        public ProjectTasksService(IRepository<ProjectTasks> projectTasksRepository)
        {
            _projectTasksRepository = projectTasksRepository;
        }

        public async Task<int> ProjectAssign(ProjectAssignRequestDto model)
        {
            var projectTasks = new ProjectMembers();
            projectTasks.ProjectId = model.ProjectId;
            projectTasks.RoleInProject = (int)model.RoleInProject;
            projectTasks.UserId = model.UserId;
            projectTasks.CreatedAt = DateTime.Now;
            projectTasks.UpdateAt = DateTime.Now;


            return 0;
        }
    }
}
