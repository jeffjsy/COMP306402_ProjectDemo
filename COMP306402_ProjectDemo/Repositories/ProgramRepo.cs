using COMP306402_ProjectDemo.Data;
using COMP306402_ProjectDemo.Models;
using Microsoft.EntityFrameworkCore;

namespace COMP306402_ProjectDemo.Repositories
{
    public class ProgramRepository : IProgramRepository
    {
        private readonly ApplicationDbContext _context;

        public ProgramRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<AcademicProgram>> GetAllAsync()
        {
            return await _context.AcedemicPrograms
                .Include(p => p.Enrollments)
                .ToListAsync();
        }

        public async Task<AcademicProgram?> GetByIdAsync(int id)
        {
            return await _context.AcedemicPrograms
                .Include(p => p.Enrollments)
                .FirstOrDefaultAsync(p => p.ProgramId == id);
        }

        public async Task<AcademicProgram> AddAsync(AcademicProgram program)
        {
            _context.AcedemicPrograms.Add(program);
            await _context.SaveChangesAsync();
            return program;
        }

        public async Task UpdateAsync(AcademicProgram program)
        {
            _context.Entry(program).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var program = await _context.AcedemicPrograms.FindAsync(id);
            if (program != null)
            {
                _context.AcedemicPrograms.Remove(program);
                await _context.SaveChangesAsync();
            }
        }
    }
}
