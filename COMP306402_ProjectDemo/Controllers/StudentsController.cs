using AutoMapper;
using COMP306402_ProjectDemo.DTO;
using COMP306402_ProjectDemo.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace COMP306402_ProjectDemo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentRepository _repo;
        private readonly IMapper _mapper;

        public StudentsController(IStudentRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        // GET: api/Students
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StudentReadDTO>>> GetStudents()
        {
            var students = await _repo.GetAllAsync();
            return Ok(_mapper.Map<List<StudentReadDTO>>(students));
        }

        // GET: api/Students/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<StudentReadDTO>> GetStudent(int id)
        {
            var student = await _repo.GetByIdAsync(id);
            if (student == null)
                return NotFound();

            return Ok(_mapper.Map<StudentReadDTO>(student));
        }

        // POST: api/Students
        [HttpPost]
        public async Task<ActionResult<StudentReadDTO>> CreateStudent(StudentCreateDTO dto)
        {
            var student = _mapper.Map<Models.Student>(dto);

            await _repo.AddAsync(student);

            var studentDTO = _mapper.Map<StudentReadDTO>(student);
            return CreatedAtAction(nameof(GetStudent), new { id = student.StudentId }, studentDTO);
        }

        // PUT: api/Students/5
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateStudent(int id, StudentUpdateDTO dto)
        {
            if (id != dto.StudentId)
                return BadRequest("ID in route does not match ID in body.");

            var student = _mapper.Map<Models.Student>(dto);

            await _repo.UpdateAsync(student);

            return NoContent();
        }

        // PATCH: api/Students/5
        [HttpPatch("{id:int}")]
        public async Task<IActionResult> PatchStudent(int id, StudentUpdateDTO dto)
        {
            var existing = await _repo.GetByIdAsync(id);
            if (existing == null)
                return NotFound();

            // Apply partial updates
            if (dto.FirstName != null)
                existing.FirstName = dto.FirstName;

            if (dto.LastName != null)
                existing.LastName = dto.LastName;

            if (dto.EnrollmentDate.HasValue)
                existing.EnrollmentDate = dto.EnrollmentDate.Value;

            // Save changes
            await _repo.UpdateAsync(existing);

            return NoContent();
        }


        // DELETE: api/Students/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            await _repo.DeleteAsync(id);
            return NoContent();
        }
    }
}
