using Infrastructure.Enums;

namespace Domain.Dto.ProjectTasks
{
    public class ProjectAssignRequestDto
    {
        public int ProjectId { get; set; }
        public int UserId { get; set; }
        public ProjectRoleEnum RoleInProject { get; set; }
    }
}
