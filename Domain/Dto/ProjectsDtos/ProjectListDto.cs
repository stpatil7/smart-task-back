using Domain.Dto.ProjectCreateDtos;

namespace Domain.Dto.ProjectsDtos
{
    public class ProjectListDto
    {
        public int total { get; set; }
        public List<ProjectResponseDto>? projects { get; set; }
    }
}
