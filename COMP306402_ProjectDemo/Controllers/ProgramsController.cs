using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using COMP306402_ProjectDemo.Data;   // adjust if needed
using COMP306402_ProjectDemo.Models; // adjust if needed

namespace COMP306402_ProjectDemo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProgramsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ProgramsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Programs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AcademicProgram>>> GetPrograms()
        {
            var programs = await _context.AcedemicPrograms
                .Include(p => p.Enrollments)
                .ToListAsync();

            return Ok(programs);
        }

        // GET: api/Programs/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<AcademicProgram>> GetProgram(int id)
        {
            var program = await _context.AcedemicPrograms
                .Include(p => p.Enrollments)
                .FirstOrDefaultAsync(p => p.ProgramId == id);

            if (program == null)
            {
                return NotFound();
            }

            return Ok(program);
        }

        // POST: api/Programs
        [HttpPost]
        public async Task<ActionResult<AcademicProgram>> CreateProgram([FromBody] AcademicProgram program)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.AcedemicPrograms.Add(program);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetProgram), new { id = program.ProgramId }, program);
        }

        // PUT: api/Programs/5
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateProgram(int id, [FromBody] AcademicProgram program)
        {
            if (id != program.ProgramId)
            {
                return BadRequest("Route id and body id must match.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Entry(program).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                var exists = await _context.AcedemicPrograms.AnyAsync(p => p.ProgramId == id);
                if (!exists)
                {
                    return NotFound();
                }

                throw;
            }

            return NoContent();
        }

        // DELETE: api/Programs/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteProgram(int id)
        {
            var program = await _context.AcedemicPrograms.FindAsync(id);

            if (program == null)
            {
                return NotFound();
            }

            _context.AcedemicPrograms.Remove(program);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
