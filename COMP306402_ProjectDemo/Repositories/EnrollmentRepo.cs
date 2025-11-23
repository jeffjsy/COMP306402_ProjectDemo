using COMP306402_ProjectDemo.Data;
using COMP306402_ProjectDemo.Models;
using Microsoft.EntityFrameworkCore;

namespace COMP306402_ProjectDemo.Repositories
{
    public class EnrollmentRepository : IEnrollmentRepository
    {
        private readonly ApplicationDbContext _context;

        public EnrollmentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Enrollment>> GetAllAsync()
        {
            return await _context.Enrollments
                .Include(e => e.Student)
                .Include(e => e.AcedemicProgram)
                .ToListAsync();
        }

        public async Task<Enrollment?> GetByIdAsync(int id)
        {
            return await _context.Enrollments
                .Include(e => e.Student)
                .Include(e => e.AcedemicProgram)
                .FirstOrDefaultAsync(e => e.EnrollmentId == id);
        }

        public async Task<Enrollment> AddAsync(Enrollment enrollment)
        {
            _context.Enrollments.Add(enrollment);
            await _context.SaveChangesAsync();
            return enrollment;
        }

        public async Task UpdateAsync(Enrollment enrollment)
        {
            _context.Entry(enrollment).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var enrollment = await _context.Enrollments.FindAsync(id);
            if (enrollment != null)
            {
                _context.Enrollments.Remove(enrollment);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Enrollment>> GetByStudentIdAsync(int studentId)
        {
            return await _context.Enrollments
                .Where(e => e.StudentId == studentId)
                .Include(e => e.Student)
                .Include(e => e.AcedemicProgram)
                .ToListAsync();
        }

        public async Task<List<Enrollment>> GetByProgramIdAsync(int programId)
        {
            return await _context.Enrollments
                .Where(e => e.ProgramId == programId)
                .Include(e => e.Student)
                .Include(e => e.AcedemicProgram)
                .ToListAsync();
        }

        public async Task<List<Enrollment>> GetByStatusAsync(string status)
        {
            return await _context.Enrollments
                .Where(e => e.Status == status)
                .Include(e => e.Student)
                .Include(e => e.AcedemicProgram)
                .ToListAsync();
        }
    }
}
