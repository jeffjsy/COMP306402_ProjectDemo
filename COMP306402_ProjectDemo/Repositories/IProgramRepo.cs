using COMP306402_ProjectDemo.Models;

namespace COMP306402_ProjectDemo.Repositories
{
    public interface IProgramRepository
    {
        Task<List<AcademicProgram>> GetAllAsync();
        Task<AcademicProgram?> GetByIdAsync(int id);
        Task<AcademicProgram> AddAsync(AcademicProgram program);
        Task UpdateAsync(AcademicProgram program);
        Task DeleteAsync(int id);
    }
}
