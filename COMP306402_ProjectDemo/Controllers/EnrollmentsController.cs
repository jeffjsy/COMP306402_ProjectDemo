using AutoMapper;
using COMP306402_ProjectDemo.DTO;
using COMP306402_ProjectDemo.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace COMP306402_ProjectDemo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EnrollmentsController : ControllerBase
    {
        private readonly IEnrollmentRepository _repo;
        private readonly IMapper _mapper;

        public EnrollmentsController(IEnrollmentRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        // GET: api/Enrollments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EnrollmentReadDTO>>> GetEnrollments()
        {
            var enrollments = await _repo.GetAllAsync();
            return Ok(_mapper.Map<List<EnrollmentReadDTO>>(enrollments));
        }

        // GET: api/Enrollments/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<EnrollmentReadDTO>> GetEnrollment(int id)
        {
            var enrollment = await _repo.GetByIdAsync(id);
            if (enrollment == null)
                return NotFound();

            return Ok(_mapper.Map<EnrollmentReadDTO>(enrollment));
        }

        // GET: api/Enrollments/byStudent/3
        [HttpGet("byStudent/{studentId:int}")]
        public async Task<ActionResult<IEnumerable<EnrollmentReadDTO>>> GetByStudent(int studentId)
        {
            var items = await _repo.GetByStudentIdAsync(studentId);
            return Ok(_mapper.Map<List<EnrollmentReadDTO>>(items));
        }

        // GET: api/Enrollments/byProgram/2
        [HttpGet("byProgram/{programId:int}")]
        public async Task<ActionResult<IEnumerable<EnrollmentReadDTO>>> GetByProgram(int programId)
        {
            var items = await _repo.GetByProgramIdAsync(programId);
            return Ok(_mapper.Map<List<EnrollmentReadDTO>>(items));
        }

        // GET: api/Enrollments/byStatus/Active
        [HttpGet("byStatus/{status}")]
        public async Task<ActionResult<IEnumerable<EnrollmentReadDTO>>> GetByStatus(string status)
        {
            var items = await _repo.GetByStatusAsync(status.Trim());
            return Ok(_mapper.Map<List<EnrollmentReadDTO>>(items));
        }

        // POST: api/Enrollments
        [HttpPost]
        public async Task<ActionResult<EnrollmentReadDTO>> CreateEnrollment(EnrollmentCreateDTO dto)
        {
            var enrollment = _mapper.Map<Models.Enrollment>(dto);

            await _repo.AddAsync(enrollment);

            return CreatedAtAction(nameof(GetEnrollment),
                new { id = enrollment.EnrollmentId },
                _mapper.Map<EnrollmentReadDTO>(enrollment));
        }

        // PUT: api/Enrollments/5
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateEnrollment(int id, EnrollmentUpdateDTO dto)
        {
            if (id != dto.EnrollmentId)
                return BadRequest("ID mismatch.");

            var enrollment = _mapper.Map<Models.Enrollment>(dto);

            await _repo.UpdateAsync(enrollment);

            return NoContent();
        }

        // PATCH: api/Enrollments/5
        [HttpPatch("{id:int}")]
        public async Task<IActionResult> PatchEnrollment(int id, EnrollmentUpdateDTO dto)
        {
            var existing = await _repo.GetByIdAsync(id);
            if (existing == null)
                return NotFound();

            if (dto.EnrollmentDate.HasValue)
                existing.EnrollmentDate = dto.EnrollmentDate.Value;

            if (dto.Status != null)
                existing.Status = dto.Status;

            if (dto.StudentId.HasValue)
                existing.StudentId = dto.StudentId.Value;

            if (dto.ProgramId.HasValue)
                existing.ProgramId = dto.ProgramId.Value;

            await _repo.UpdateAsync(existing);

            return NoContent();
        }

        // DELETE: api/Enrollments/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteEnrollment(int id)
        {
            await _repo.DeleteAsync(id);
            return NoContent();
        }
    }
}
