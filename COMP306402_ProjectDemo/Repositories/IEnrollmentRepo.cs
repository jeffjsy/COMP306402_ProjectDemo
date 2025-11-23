using COMP306402_ProjectDemo.Models;

namespace COMP306402_ProjectDemo.Repositories
{
    public interface IEnrollmentRepository
    {
        Task<List<Enrollment>> GetAllAsync();
        Task<Enrollment?> GetByIdAsync(int id);
        Task<Enrollment> AddAsync(Enrollment enrollment);
        Task UpdateAsync(Enrollment enrollment);
        Task DeleteAsync(int id);

        Task<List<Enrollment>> GetByStudentIdAsync(int studentId);
        Task<List<Enrollment>> GetByProgramIdAsync(int programId);
        Task<List<Enrollment>> GetByStatusAsync(string status);
    }
}
