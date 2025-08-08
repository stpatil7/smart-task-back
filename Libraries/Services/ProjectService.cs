using DataAccessLayer.Entities;
using Domain.Dto.ProjectCreateDtos;
using Domain.Dto.ProjectsDtos;
using Domain.Interfaces.ProjectCreate;
using Domain.Interfaces.Repository;
using Microsoft.EntityFrameworkCore;

namespace Application.Services
{
    public class ProjectService : IProjectService
    {
        private readonly IRepository<Projects> _projectsRepository;
        public ProjectService(IRepository<Projects> projectsRepository)
        {
            _projectsRepository = projectsRepository;
        }

        public async Task<int> CreateProject(ProjectCreateRequestModel model)
        {
            Projects proj = new Projects();

            proj.Name = model.Name;
            proj.Description = model.Description;
            proj.StartDate = model.StartDate;
            proj.EndDate = model.EndDate;
            proj.CreatedById = model.CreatedById;
            proj.CreatedAt = DateTime.Now;
            proj.UpdatedAt = DateTime.Now;

            await _projectsRepository.AddAsync(proj);
            await _projectsRepository.SaveAsync();

            return proj.Id;
        }


        public async Task<ProjectList> GetProjects(ProjectRequest model)
        {
            var query = _projectsRepository.GetAllAsync().Where(x => x.CreatedById == model.userId);

            var projects = await query.Take(model.Take).Skip(model.Skip).ToListAsync();

            var count = await query.CountAsync();

            var result = new ProjectList
            {
                total = count,
                projects = projects.Select(items => new ProjectResponse
                {
                    Id = items.Id,
                    Name = items.Name,
                    Description = items.Description,
                    StartDate = items.StartDate,
                    EndDate = items.EndDate,
                    CreatedById = items.CreatedById,
                    CreatedAt = items.CreatedAt,
                    UpdatedAt = items.UpdatedAt,
                    DeletedAt = items.DeletedAt
                }).ToList()
            };
            return result;
        }

        public async Task<ProjectResponse> GetProjectById(int id)
        {
            var project = await _projectsRepository.GetByIdAsync(id);

            if (project == null)
                return null;

            return new ProjectResponse
            {
                Id = project.Id,
                Name = project.Name,
                Description = project.Description,
                StartDate = project.StartDate,
                EndDate = project.EndDate,
                CreatedById = project.CreatedById,
                CreatedAt = project.CreatedAt,
                UpdatedAt = project.UpdatedAt,
                DeletedAt = project.DeletedAt
            };
        }

    }
}
