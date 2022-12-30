using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskManagement.Models
{
    public class TaskMaterialUsage
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public virtual Task Task { get; set; }
        public Guid TaskId { get; set; }
        public virtual Material Material { get; set; }
        public Guid MaterialId { get; set; }
        public int Amount { get; set; }
        public virtual UnitMeasure UnitOfMeasurement { get; set; }
        public long UnitOfMeasurementId { get; set; }

    }
}
