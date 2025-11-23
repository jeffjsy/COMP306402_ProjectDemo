using AutoMapper;
using COMP306402_ProjectDemo.DTO;
using COMP306402_ProjectDemo.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace COMP306402_ProjectDemo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProgramsController : ControllerBase
    {
        private readonly IProgramRepository _repo;
        private readonly IMapper _mapper;

        public ProgramsController(IProgramRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        // GET: api/Programs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProgramReadDTO>>> GetPrograms()
        {
            var programs = await _repo.GetAllAsync();
            return Ok(_mapper.Map<List<ProgramReadDTO>>(programs));
        }

        // GET: api/Programs/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<ProgramReadDTO>> GetProgram(int id)
        {
            var program = await _repo.GetByIdAsync(id);
            if (program == null)
                return NotFound();

            return Ok(_mapper.Map<ProgramReadDTO>(program));
        }

        // POST: api/Programs
        [HttpPost]
        public async Task<ActionResult<ProgramReadDTO>> CreateProgram(ProgramCreateDTO dto)
        {
            var program = _mapper.Map<Models.AcademicProgram>(dto);

            await _repo.AddAsync(program);

            return CreatedAtAction(nameof(GetProgram),
                new { id = program.ProgramId },
                _mapper.Map<ProgramReadDTO>(program));
        }

        // PUT: api/Programs/5
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateProgram(int id, ProgramUpdateDTO dto)
        {
            if (id != dto.ProgramId)
                return BadRequest("ID mismatch.");

            var program = _mapper.Map<Models.AcademicProgram>(dto);

            await _repo.UpdateAsync(program);

            return NoContent();
        }


        // PATCH: api/Programs/5
        [HttpPatch("{id:int}")]
        public async Task<IActionResult> PatchProgram(int id, ProgramUpdateDTO dto)
        {
            var existing = await _repo.GetByIdAsync(id);
            if (existing == null)
                return NotFound();

            if (dto.Name != null)
                existing.Name = dto.Name;

            if (dto.Description != null)
                existing.Description = dto.Description;

            if (dto.DurationMonths.HasValue)
                existing.DurationMonths = dto.DurationMonths.Value;

            if (dto.TuitionFee.HasValue)
                existing.TuitionFee = dto.TuitionFee.Value;

            await _repo.UpdateAsync(existing);

            return NoContent();
        }


        // DELETE: api/Programs/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteProgram(int id)
        {
            await _repo.DeleteAsync(id);
            return NoContent();
        }
    }
}
