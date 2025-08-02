using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.Entities
{
    public class Users
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string PasswordHash { get; set; }
        [Required]
        public int role { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdateAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public virtual ICollection<Projects> ProjectsCreated { get; set; }
        public virtual ICollection<ProjectMembers> ProjectMemberships { get; set; }
        public virtual ICollection<ProjectTasks> AssignedTasks { get; set; }

    }
}
