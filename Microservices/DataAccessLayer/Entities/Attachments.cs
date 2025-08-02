using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer.Entities
{
    public class Attachments
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int TaskId { get; set; }
        [Required]
        public string FileName { get; set; }
        [Required]
        public string FilePath { get; set; }
        [Required]
        public int UploadedById { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdateAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        [ForeignKey("UploadedById")]
        public virtual Users User { get; set; }
        [ForeignKey("TaskId")]
        public virtual ProjectTasks ProjectTasks { get; set; }

    }
}
