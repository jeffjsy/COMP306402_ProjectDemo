using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using COMP306402_ProjectDemo.Data;
using COMP306402_ProjectDemo.Models; 

namespace COMP306402_ProjectDemo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EnrollmentsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public EnrollmentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Enrollments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Enrollment>>> GetEnrollments()
        {
            var enrollments = await _context.Enrollments
                .Include(e => e.Student)
                .Include(e => e.AcedemicProgram)
                .ToListAsync();

            return Ok(enrollments);
        }

        // GET: api/Enrollments/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Enrollment>> GetEnrollment(int id)
        {
            var enrollment = await _context.Enrollments
                .Include(e => e.Student)
                .Include(e => e.AcedemicProgram)
                .FirstOrDefaultAsync(e => e.EnrollmentId == id);

            if (enrollment == null)
            {
                return NotFound();
            }

            return Ok(enrollment);
        }

        // GET: api/Enrollments/byStudent/3
        [HttpGet("byStudent/{studentId:int}")]
        public async Task<ActionResult<IEnumerable<Enrollment>>> GetEnrollmentsByStudent(int studentId)
        {
            var enrollments = await _context.Enrollments
                .Include(e => e.Student)
                .Include(e => e.AcedemicProgram)
                .Where(e => e.StudentId == studentId)
                .ToListAsync();

            return Ok(enrollments);
        }

        // GET: api/Enrollments/byProgram/2
        [HttpGet("byProgram/{programId:int}")]
        public async Task<ActionResult<IEnumerable<Enrollment>>> GetEnrollmentsByProgram(int programId)
        {
            var enrollments = await _context.Enrollments
                .Include(e => e.Student)
                .Include(e => e.AcedemicProgram)
                .Where(e => e.ProgramId == programId)
                .ToListAsync();

            return Ok(enrollments);
        }

        // GET: api/Enrollments/byStatus/Active
        [HttpGet("byStatus/{status}")]
        public async Task<ActionResult<IEnumerable<Enrollment>>> GetEnrollmentsByStatus(string status)
        {
            var normalized = status.Trim();

            var enrollments = await _context.Enrollments
                .Include(e => e.Student)
                .Include(e => e.AcedemicProgram)
                .Where(e => e.Status == normalized)
                .ToListAsync();

            return Ok(enrollments);
        }

        // POST: api/Enrollments
        [HttpPost]
        public async Task<ActionResult<Enrollment>> CreateEnrollment([FromBody] Enrollment enrollment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Optional: validate Student and Program exist
            var studentExists = await _context.Students.AnyAsync(s => s.StudentId == enrollment.StudentId);
            var programExists = await _context.AcedemicPrograms.AnyAsync(p => p.ProgramId == enrollment.ProgramId);

            if (!studentExists || !programExists)
            {
                return BadRequest("Invalid StudentId or ProgramId.");
            }

            _context.Enrollments.Add(enrollment);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetEnrollment), new { id = enrollment.EnrollmentId }, enrollment);
        }

        // PUT: api/Enrollments/5
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateEnrollment(int id, [FromBody] Enrollment enrollment)
        {
            if (id != enrollment.EnrollmentId)
            {
                return BadRequest("Route id and body id must match.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Entry(enrollment).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                var exists = await _context.Enrollments.AnyAsync(e => e.EnrollmentId == id);
                if (!exists)
                {
                    return NotFound();
                }

                throw;
            }

            return NoContent();
        }

        // DELETE: api/Enrollments/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteEnrollment(int id)
        {
            var enrollment = await _context.Enrollments.FindAsync(id);

            if (enrollment == null)
            {
                return NotFound();
            }

            _context.Enrollments.Remove(enrollment);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
