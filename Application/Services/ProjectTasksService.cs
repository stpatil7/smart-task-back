using DataAccessLayer.Entities;
using Domain.Dto.ProjectTasks;
using Domain.Interfaces.ProjectTasks;
using Domain.Interfaces.Repository;
using Microsoft.EntityFrameworkCore;

namespace Application.Services
{
    public class ProjectTasksService : IProjectTasksService
    {
        private readonly IRepository<ProjectMembers> _projectTasksRepository;
        public ProjectTasksService(IRepository<ProjectMembers> projectTasksRepository)
        {
            _projectTasksRepository = projectTasksRepository;
        }

        public async Task<int> ProjectAssign(ProjectAssignRequestDto model)
        {
            var projectMembers = await _projectTasksRepository.Find(x => x.ProjectId == model.ProjectId).FirstOrDefaultAsync();
            if (projectMembers != null)
                throw new InvalidOperationException("This user is already assigned to the project.");

            projectMembers = new ProjectMembers();

            projectMembers.ProjectId = model.ProjectId;
            projectMembers.RoleInProject = (int)model.RoleInProject;
            projectMembers.UserId = model.UserId;
            projectMembers.CreatedAt = DateTime.Now;
            projectMembers.UpdateAt = DateTime.Now;

            await _projectTasksRepository.AddAsync(projectMembers);
            await _projectTasksRepository.SaveAsync();

            return projectMembers.Id;
        }

    }
}
