using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer.Entities
{
    public class ProjectTasks
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Status { get; set; }
        public int Priority { get; set; }
        public int DueDate { get; set; }
        [Required]
        public int ProjectId { get; set; }
        public int? AssignedToId { get; set; }
        [Required]
        public int CreatedById { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdateAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        [ForeignKey("ProjectId")]
        public virtual Projects Project { get; set; }
        [ForeignKey("AssignedToId")]
        public virtual Users AssignedTo { get; set; }
        [ForeignKey("CreatedById")]
        public virtual Users CreatedBy { get; set; }
    }
}
