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

        public async Task<int> CreateProject(ProjectCreateRequestDto model)
        {
            var proj = await _projectsRepository.Find(x => x.Name == model.Name).FirstOrDefaultAsync();

            if (proj != null)
                throw new InvalidOperationException("This project is already created.");

            proj = new Projects();

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


        public async Task<ProjectListDto> GetProjects(ProjectRequestDto model)
        {
            var query = _projectsRepository.GetAllAsync().Where(x => x.CreatedById == model.userId && x.DeletedAt == null);

            var projects = await query.Take(model.Take).Skip(model.Skip).ToListAsync();

            var count = await query.CountAsync();

            var result = new ProjectListDto
            {
                total = count,
                projects = projects.Select(items => new ProjectResponseDto
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

        public async Task<ProjectResponseDto> GetProjectById(int id)
        {
            var project = await _projectsRepository.Find(x => x.Id == id && x.DeletedAt == null).FirstOrDefaultAsync();

            if (project == null)
                throw new InvalidOperationException("Project not found with id" + id);

            return new ProjectResponseDto
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

        public async Task<bool> DeleteProject(int id)
        {
            var project = await _projectsRepository.Find(x => x.Id == id && x.DeletedAt == null).FirstOrDefaultAsync();
            if (project == null) return false;

            project.DeletedAt = DateTime.UtcNow;
            _projectsRepository.UpdateAsync(project);
            await _projectsRepository.SaveAsync();

            return true;
        }

        public async Task<bool> UpdateProject(int id, ProjectUpdateRequestDto model)
        {
            var project = await _projectsRepository.Find(x => x.Id == id).FirstOrDefaultAsync();
            if (project == null) return false;

            project.Name = model.Name;
            project.Description = model.Description;
            project.EndDate = model.EndDate;
            project.UpdatedAt = DateTime.Now;

            _projectsRepository.UpdateAsync(project);
            await _projectsRepository.SaveAsync();

            return true;
        }

    }
}
