using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskManagement.Models
{
    public class Task
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        [Required(AllowEmptyStrings = false)]
        public string Name { get; set; }
        public int Description { get; set; }
        public int TotalDuration { get; set; }
        public virtual Material Material { get; set; }
        public Guid MaterialId { get; set; }


    }
}
