using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskManagement.Models;
using M = TaskManagement.Models;

namespace TaskManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskMaterialUsagesController : ControllerBase
    {
        private readonly TaskManagementContext _context;

        public TaskMaterialUsagesController(TaskManagementContext context)
        {
            _context = context;
        }

        // GET: api/TaskMaterialUsages
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaskMaterialUsage>>> GetTaskMaterialUsage()
        {
            return await _context.TaskMaterialUsage.ToListAsync();
        }

        // GET: api/TaskMaterialUsages/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TaskMaterialUsage>> GetTaskMaterialUsage(Guid id)
        {
            var taskMaterialUsage =  _context.TaskMaterialUsage.Where(x => x.Id == id)
                .Include(x => x.Task)
                .Include(x => x.Material)
                .Include(x => x.UnitOfMeasurement)
                .FirstOrDefault();

            if (taskMaterialUsage == null)
            {
                return NotFound();
            }

            return taskMaterialUsage;
        }
        // GET: api/TaskMaterialUsages/5
        [HttpGet("{taskId}/{materialId}")]
        public async Task<ActionResult<TaskMaterialUsage>> GetTaskMaterialUsageByTaskAndMaterial(Guid taskId, Guid materialId)
        {
            var taskMaterialUsage = _context.TaskMaterialUsage.Where(x => x.TaskId == taskId && x.MaterialId == materialId)
                .FirstOrDefault();

            if (taskMaterialUsage == null)
            {
                return NotFound();
            }

            return taskMaterialUsage;
        }

        // PUT: api/TaskMaterialUsages/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTaskMaterialUsage(Guid id, TaskMaterialUsage taskMaterialUsage)
        {
            if (id != taskMaterialUsage.Id)
            {
                return BadRequest();
            }

            _context.Entry(taskMaterialUsage).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TaskMaterialUsageExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/TaskMaterialUsages
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public ActionResult<TaskMaterialUsage> PostTaskMaterialUsage(TaskMaterialUsage taskMaterialUsage)
        {

            var tmu = _context.TaskMaterialUsage.Where(x => x.Id == taskMaterialUsage.Id).FirstOrDefault();
            // a TaskMaterialUsage exixt, just update the data
            if (tmu!=null)
            {
                
                tmu.Task.Name = taskMaterialUsage.Task.Name;
                tmu.Task.Description = taskMaterialUsage.Task.Description;
                tmu.Task.TotalDuration = taskMaterialUsage.Task.TotalDuration;

                tmu.Amount = taskMaterialUsage.Amount;
                tmu.UnitOfMeasurementId = taskMaterialUsage.UnitOfMeasurement.Id;

                _context.TaskMaterialUsage.Update(tmu);
                _context.Tasks.Update(tmu.Task);
                
            }
            else
            {
                var tmu_new = new TaskMaterialUsage();
                var task = _context.Tasks.Find(taskMaterialUsage.Task.Id);

                tmu_new.Amount= taskMaterialUsage.Amount;
                tmu_new.UnitOfMeasurementId = taskMaterialUsage.UnitOfMeasurement.Id;
                tmu_new.MaterialId = taskMaterialUsage.Task.Material.Id;

                // if task not exist, create it
                if (task == null)
                {
                    task = new M.Task();
                    task.Id = Guid.NewGuid();
                    task.MaterialId = taskMaterialUsage.Task.Material.Id;
                    task.Name = taskMaterialUsage.Task.Name;
                    task.Description = taskMaterialUsage.Task.Description;
                    task.TotalDuration = taskMaterialUsage.Task.TotalDuration;
                    _context.Tasks.Add(task);
                }
                // if task exist, just update it
                else
                {
                    task.Name = taskMaterialUsage.Task.Name;
                    task.Description = taskMaterialUsage.Task.Description;
                    task.TotalDuration = taskMaterialUsage.Task.TotalDuration;
                    _context.Tasks.Update(task);
                }

                tmu_new.TaskId = task.Id;

                _context.TaskMaterialUsage.Add(tmu_new);

                tmu = tmu_new;
            }

            _context.SaveChanges();

            return CreatedAtAction("GetTaskMaterialUsage", new { id = tmu.Id }, tmu);
        }

        // DELETE: api/TaskMaterialUsages/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTaskMaterialUsage(Guid id)
        {
            var taskMaterialUsage = await _context.TaskMaterialUsage.FindAsync(id);
            if (taskMaterialUsage == null)
            {
                return NotFound();
            }

            _context.TaskMaterialUsage.Remove(taskMaterialUsage);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TaskMaterialUsageExists(Guid id)
        {
            return _context.TaskMaterialUsage.Any(e => e.Id == id);
        }
    }
}
