using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace EnrollmentPortal.Services
{
    public class SchoolApiClient
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _jsonOptions;

        public SchoolApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _jsonOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                PropertyNameCaseInsensitive = true
            };
        }

        // -------------------------
        // STUDENTS - 6 METHODS
        // -------------------------

        // GET (GetAll)
        public async Task<List<StudentReadDTO>> GetAllStudentsAsync()
        {
            var result = await _httpClient.GetAsync("api/Students");
            result.EnsureSuccessStatusCode();

            var students = await result.Content.ReadFromJsonAsync<List<StudentReadDTO>>(_jsonOptions);
            return students ?? new List<StudentReadDTO>();
        }

        // GET (GetById)
        public async Task<StudentReadDTO?> GetStudentByIdAsync(int id)
        {
            var result = await _httpClient.GetAsync($"api/Students/{id}");
            if (result.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }

            result.EnsureSuccessStatusCode();
            return await result.Content.ReadFromJsonAsync<StudentReadDTO>(_jsonOptions);
        }

        // POST - Create
        public async Task<StudentReadDTO?> CreateStudentAsync(StudentCreateDTO student)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Students", student, _jsonOptions);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<StudentReadDTO>(_jsonOptions);
        }

        // PUT - Replace
        public async Task<bool> UpdateStudentAsync(int id, StudentUpdateDTO student)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/Students/{id}", student, _jsonOptions);
            return response.IsSuccessStatusCode;
        }

        // PATCH - Partial update
        public async Task<bool> PatchStudentAsync(int id, JsonDocument patchDoc)
        {
            var content = new StringContent(patchDoc.RootElement.GetRawText(), Encoding.UTF8, "application/json");
            var response = await _httpClient.PatchAsync($"api/Students/{id}", content);
            return response.IsSuccessStatusCode;
        }

        // DELETE - Remove
        public async Task<bool> DeleteStudentAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/Students/{id}");
            return response.IsSuccessStatusCode;
        }

        // -------------------------
        // PROGRAMS - 6 METHODS
        // -------------------------

        // GET (GetAll)
        public async Task<List<ProgramReadDTO>> GetAllProgramsAsync()
        {
            var result = await _httpClient.GetAsync("api/Programs");
            result.EnsureSuccessStatusCode();

            var programs = await result.Content.ReadFromJsonAsync<List<ProgramReadDTO>>(_jsonOptions);
            return programs ?? new List<ProgramReadDTO>();
        }

        // GET (GetById)
        public async Task<ProgramReadDTO?> GetProgramByIdAsync(int id)
        {
            var result = await _httpClient.GetAsync($"api/Programs/{id}");
            if (result.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }

            result.EnsureSuccessStatusCode();
            return await result.Content.ReadFromJsonAsync<ProgramReadDTO>(_jsonOptions);
        }

        // POST - Create
        public async Task<ProgramReadDTO?> CreateProgramAsync(ProgramCreateDTO program)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Programs", program, _jsonOptions);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<ProgramReadDTO>(_jsonOptions);
        }

        // PUT - Replace
        public async Task<bool> UpdateProgramAsync(int id, ProgramUpdateDTO program)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/Programs/{id}", program, _jsonOptions);
            return response.IsSuccessStatusCode;
        }

        // PATCH - Partial update
        public async Task<bool> PatchProgramAsync(int id, JsonDocument patchDoc)
        {
            var content = new StringContent(patchDoc.RootElement.GetRawText(), Encoding.UTF8, "application/json");
            var response = await _httpClient.PatchAsync($"api/Programs/{id}", content);
            return response.IsSuccessStatusCode;
        }

        // DELETE - Remove
        public async Task<bool> DeleteProgramAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/Programs/{id}");
            return response.IsSuccessStatusCode;
        }

        // -------------------------
        // ENROLLMENTS - 6 METHODS
        // -------------------------

        // GET (GetAll)
        public async Task<List<EnrollmentReadDTO>> GetAllEnrollmentsAsync()
        {
            var result = await _httpClient.GetAsync("api/Enrollments");
            result.EnsureSuccessStatusCode();

            var enrollments = await result.Content.ReadFromJsonAsync<List<EnrollmentReadDTO>>(_jsonOptions);
            return enrollments ?? new List<EnrollmentReadDTO>();
        }

        // GET (GetById)
        public async Task<EnrollmentReadDTO?> GetEnrollmentByIdAsync(int id)
        {
            var result = await _httpClient.GetAsync($"api/Enrollments/{id}");
            if (result.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }

            result.EnsureSuccessStatusCode();
            return await result.Content.ReadFromJsonAsync<EnrollmentReadDTO>(_jsonOptions);
        }

        // POST - Create
        public async Task<EnrollmentReadDTO?> CreateEnrollmentAsync(EnrollmentCreateDTO enrollment)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Enrollments", enrollment, _jsonOptions);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<EnrollmentReadDTO>(_jsonOptions);
        }

        // PUT - Replace
        public async Task<bool> UpdateEnrollmentAsync(int id, EnrollmentUpdateDTO enrollment)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/Enrollments/{id}", enrollment, _jsonOptions);
            return response.IsSuccessStatusCode;
        }

        // PATCH - Partial update
        public async Task<bool> PatchEnrollmentAsync(int id, JsonDocument patchDoc)
        {
            var content = new StringContent(patchDoc.RootElement.GetRawText(), Encoding.UTF8, "application/json");
            var response = await _httpClient.PatchAsync($"api/Enrollments/{id}", content);
            return response.IsSuccessStatusCode;
        }

        // DELETE - Remove
        public async Task<bool> DeleteEnrollmentAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/Enrollments/{id}");
            return response.IsSuccessStatusCode;
        }
    }

    // DTOs for Students
    public class StudentReadDTO
    {
        public int StudentId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime EnrollmentDate { get; set; }
    }

    public class StudentCreateDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime EnrollmentDate { get; set; }
    }

    public class StudentUpdateDTO
    {
        public int? StudentId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime? EnrollmentDate { get; set; }
    }

    public class EnrollmentCreateDTO
    {
        public DateTime EnrollmentDate { get; set; }
        public string Status { get; set; }
        public int StudentId { get; set; }
        public int ProgramId { get; set; }
    }

    public class EnrollmentReadDTO
    {
        public int EnrollmentId { get; set; }
        public DateTime EnrollmentDate { get; set; }
        public string Status { get; set; }
        public int StudentId { get; set; }
        public int ProgramId { get; set; }
    }

    public class EnrollmentUpdateDTO
    {
        public int? EnrollmentId { get; set; }
        public DateTime? EnrollmentDate { get; set; }
        public string? Status { get; set; }
        public int? StudentId { get; set; }
        public int? ProgramId { get; set; }
    }

    public class ProgramCreateDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int DurationMonths { get; set; }
        public decimal TuitionFee { get; set; }
    }

    public class ProgramReadDTO
    {
        public int ProgramId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int DurationMonths { get; set; }
        public decimal TuitionFee { get; set; }
    }

    public class ProgramUpdateDTO
    {
        public int? ProgramId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int? DurationMonths { get; set; }
        public decimal? TuitionFee { get; set; }
    }
}
