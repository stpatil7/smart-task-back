using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer.Entities
{
    public class ActivityLog
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public string Action { get; set; }
        [Required]
        public string EntityType { get; set; }
        [Required]
        public int EntityId { get; set; }
        public DateTime TimeStamp { get; set; }
        [ForeignKey("UserId")]
        public virtual Users Users { get; set; }
    }
}
