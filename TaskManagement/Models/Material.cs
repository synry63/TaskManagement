using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace TaskManagement.Models
{
    public class Material
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        [Required(AllowEmptyStrings = false)]
        public string Partnumber { get; set; }
        public int ManufacturerCode { get; set; }
        public int Price { get; set; }
        public virtual UnitMeasure UnitOfIssue { get; set; }
        public long UnitOfIssueId { get; set; }


        //public virtual ICollection<Task> Tasks { get; set; }

        //public virtual ICollection<TaskMaterialUsage> TaskMaterialUsages { get; set; }


    }
}
