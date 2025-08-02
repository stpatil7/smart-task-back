using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer.Entities
{
    public class Projects
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        [Required]
        public int CreatedById { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        [ForeignKey("CreatedById")]
        public virtual Users CreatedBy { get; set; }

        public virtual ICollection<ProjectTasks> Tasks { get; set; }

        public virtual ICollection<ProjectMembers> ProjectMembers { get; set; }
    }
}
