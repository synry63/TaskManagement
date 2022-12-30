using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskManagement.Models;

namespace TaskManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UnitMeasuresController : ControllerBase
    {
        private readonly TaskManagementContext _context;

        public UnitMeasuresController(TaskManagementContext context)
        {
            _context = context;
        }

        // GET: api/UnitMeasures
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UnitMeasure>>> GetUnitMeasure()
        {
            return await _context.UnitMeasure.ToListAsync();
        }

        // GET: api/UnitMeasures/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UnitMeasure>> GetUnitMeasure(long id)
        {
            var unitMeasure = await _context.UnitMeasure.FindAsync(id);

            if (unitMeasure == null)
            {
                return NotFound();
            }

            return unitMeasure;
        }

        // PUT: api/UnitMeasures/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUnitMeasure(long id, UnitMeasure unitMeasure)
        {
            if (id != unitMeasure.Id)
            {
                return BadRequest();
            }

            _context.Entry(unitMeasure).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UnitMeasureExists(id))
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

        // POST: api/UnitMeasures
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<UnitMeasure>> PostUnitMeasure(UnitMeasure unitMeasure)
        {
            _context.UnitMeasure.Add(unitMeasure);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUnitMeasure", new { id = unitMeasure.Id }, unitMeasure);
        }

        // DELETE: api/UnitMeasures/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUnitMeasure(long id)
        {
            var unitMeasure = await _context.UnitMeasure.FindAsync(id);
            if (unitMeasure == null)
            {
                return NotFound();
            }

            _context.UnitMeasure.Remove(unitMeasure);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UnitMeasureExists(long id)
        {
            return _context.UnitMeasure.Any(e => e.Id == id);
        }
    }
}
