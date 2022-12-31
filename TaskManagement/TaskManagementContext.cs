
using Microsoft.EntityFrameworkCore;
using TaskManagement.Models;
using M = TaskManagement.Models;
namespace TaskManagement
{
    public class TaskManagementContext : DbContext
    {
        public TaskManagementContext(DbContextOptions<TaskManagementContext> options)
            : base(options)
        {
        }

        public DbSet<M.Material> Materials { get; set; }
        public DbSet<M.Task> Tasks { get; set; }
        public DbSet<TaskMaterialUsage> TaskMaterialUsage { get; set; }
        public DbSet<UnitMeasure> UnitMeasure { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<M.Task>()
            .HasOne(c => c.Material)
            .WithMany()
            .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<TaskMaterialUsage>()
            .HasOne(c => c.Material)
            .WithMany()
            .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<TaskMaterialUsage>()
            .HasOne(c => c.Task)
            .WithMany()
            .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Material>()
             .HasOne(c => c.UnitOfIssue)
             .WithMany()
             .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<TaskMaterialUsage>()
             .HasOne(c => c.UnitOfMeasurement)
             .WithMany()
             .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Material>().Property(e => e.Id).HasDefaultValueSql("newid()");
            modelBuilder.Entity<M.Task>().Property(e => e.Id).HasDefaultValueSql("newid()");
            modelBuilder.Entity<TaskMaterialUsage>().Property(e => e.Id).HasDefaultValueSql("newid()");


            //Seed Data for testing
            modelBuilder.Entity<UnitMeasure>().HasData(
                 new UnitMeasure { Id = 1, Name = "liters", Sort = 1 },
                 new UnitMeasure { Id = 2, Name = "meters", Sort = 2 },
                 new UnitMeasure { Id = 3, Name = "milliliters", Sort = 3 },
                 new UnitMeasure { Id = 4, Name = "centimeters", Sort = 4 }
            );

            Guid[] guidsMaterials = new Guid[] { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() };
            modelBuilder.Entity<Material>().HasData(
                new Material { Id = guidsMaterials[0], ManufacturerCode = 48, Partnumber = "AX778", Price = 78, UnitOfIssueId = 1 },
                new Material { Id = guidsMaterials[1], ManufacturerCode = 778, Partnumber = "LX777", Price = 108, UnitOfIssueId = 2 },
                new Material { Id = guidsMaterials[2], ManufacturerCode = 66549, Partnumber = "PO846", Price = 999, UnitOfIssueId = 3 }
            );
            Guid[] guidsTasks = new Guid[] { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() };
            modelBuilder.Entity<M.Task>().HasData(
                new M.Task { Id = guidsTasks[0], Description = 66, Name = "Reparation FTX", TotalDuration = 60, MaterialId = guidsMaterials[0] },
                new M.Task { Id = guidsTasks[1], Description = 777, Name = "Filtrage TAX", TotalDuration = 30, MaterialId = guidsMaterials[1] },
                new M.Task { Id = guidsTasks[2], Description = 63549, Name = "Assemblage FII", TotalDuration = 10, MaterialId = guidsMaterials[2] }
            );

            modelBuilder.Entity<TaskMaterialUsage>().HasData(
                new TaskMaterialUsage { Id = Guid.NewGuid(), Amount = 100, UnitOfMeasurementId = 3, MaterialId = guidsMaterials[0], TaskId = guidsTasks[0] },
                new TaskMaterialUsage { Id = Guid.NewGuid(), Amount = 30, UnitOfMeasurementId = 4, MaterialId = guidsMaterials[1], TaskId = guidsTasks[1] },
                new TaskMaterialUsage { Id = Guid.NewGuid(), Amount = 5000, UnitOfMeasurementId = 1, MaterialId = guidsMaterials[2], TaskId = guidsTasks[2] }
            );

        }


    }
}
