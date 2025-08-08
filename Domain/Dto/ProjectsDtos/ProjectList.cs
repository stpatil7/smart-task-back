using Domain.Dto.ProjectCreateDtos;

namespace Domain.Dto.ProjectsDtos
{
    public class ProjectList
    {
        public int total { get; set; }
        public List<ProjectResponse>? projects { get; set; }
    }
}
