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
    public class MaterialsController : ControllerBase
    {
        private readonly TaskManagementContext _context;

        public MaterialsController(TaskManagementContext context)
        {
            _context = context;
        }

        // GET: api/Materials
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Material>>> GetMaterials()
        {
            return await _context.Materials.OrderBy(x => x.Partnumber).ToListAsync();
        }

        // GET: api/Materials/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Material>> GetMaterial(Guid id)
        {


            var material = await _context.Materials.FindAsync(id);

            if (material == null)
            {
                return NotFound();
            }

            return material;
        }

        // PUT: api/Materials/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMaterial(Guid id, Material material)
        {
            if (id != material.Id)
            {
                return BadRequest();
            }

            _context.Entry(material).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MaterialExists(id))
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

        // POST: api/Materials
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public ActionResult<Material> PostMaterial(Material material)
        {

            var mat = _context.Materials.Where(x => x.Id == material.Id).FirstOrDefault();
            if (mat!=null)
            {
                mat.Partnumber = material.Partnumber;
                mat.ManufacturerCode = material.ManufacturerCode;
                mat.Price = material.Price;
                _context.Update(mat);
            }
            else{
                mat = new Material();
                mat.Partnumber = material.Partnumber;
                mat.ManufacturerCode = material.ManufacturerCode;
                mat.Price = material.ManufacturerCode;
                mat.UnitOfIssueId = material.UnitOfIssue.Id;

                _context.Add(mat);
            }
            
            _context.SaveChanges();

            return CreatedAtAction("GetMaterial", new { id = mat.Id }, mat);
        }

        // DELETE: api/Materials/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Material>> DeleteMaterial(Guid id)
        {
            var material = await _context.Materials.FindAsync(id);
            if (material == null)
            {
                return NotFound();
            }

            _context.Materials.Remove(material);
            await _context.SaveChangesAsync();

            return material;
        }

        private bool MaterialExists(Guid id)
        {
            return _context.Materials.Any(e => e.Id == id);
        }
    }
}
