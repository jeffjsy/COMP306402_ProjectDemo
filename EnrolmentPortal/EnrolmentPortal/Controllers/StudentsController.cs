using EnrollmentPortal.Services;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace EnrollmentPortal.Controllers
{
    public class StudentsController : Controller
    {
        private readonly SchoolApiClient _apiClient;

        public StudentsController(SchoolApiClient apiClient)
        {
            _apiClient = apiClient;
        }

        // GET /Students
        // Uses GET (GetAll)
        public async Task<IActionResult> Index()
        {
            var students = await _apiClient.GetAllStudentsAsync();
            return View(students);
        }

        // GET /Students/Details/5
        // Uses GET (GetById)
        public async Task<IActionResult> Details(int id)
        {
            var student = await _apiClient.GetStudentByIdAsync(id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // GET /Students/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST /Students/Create
        // Uses POST - Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(StudentCreateDTO model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var created = await _apiClient.CreateStudentAsync(model);
            if (created == null)
            {
                ModelState.AddModelError(string.Empty, "Failed to create student.");
                return View(model);
            }

            return RedirectToAction(nameof(Index));
        }

        // GET /Students/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var student = await _apiClient.GetStudentByIdAsync(id);
            if (student == null)
            {
                return NotFound();
            }

            var updateModel = new StudentUpdateDTO
            {
                StudentId = student.StudentId,
                FirstName = student.FirstName,
                LastName = student.LastName,
                EnrollmentDate = student.EnrollmentDate
            };

            return View(updateModel);
        }

        // POST /Students/Edit/5
        // Uses PUT - Replace
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, StudentUpdateDTO model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var success = await _apiClient.UpdateStudentAsync(id, model);
            if (!success)
            {
                ModelState.AddModelError(string.Empty, "Failed to update student.");
                return View(model);
            }

            return RedirectToAction(nameof(Details), new { id });
        }

        // GET /Students/PatchEnrollmentDate/5
        public async Task<IActionResult> PatchEnrollmentDate(int id)
        {
            var student = await _apiClient.GetStudentByIdAsync(id);
            if (student == null)
            {
                return NotFound();
            }

            ViewBag.StudentId = id;
            ViewBag.CurrentEnrollmentDate = student.EnrollmentDate;
            return View();
        }

        // POST /Students/PatchEnrollmentDate/5
        // Uses PATCH - partial update (enrollmentDate only)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PatchEnrollmentDate(int id, DateTime newDate)
        {
            using var patchJson = JsonDocument.Parse(
                $"{{\"enrollmentDate\":\"{newDate:yyyy-MM-ddTHH:mm:ss}\"}}");

            var success = await _apiClient.PatchStudentAsync(id, patchJson);

            if (!success)
            {
                ModelState.AddModelError(string.Empty, "Failed to apply patch.");
                ViewBag.StudentId = id;
                return View();
            }

            return RedirectToAction(nameof(Details), new { id });
        }

        // POST /Students/Delete/5
        // Uses DELETE - Remove
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _apiClient.DeleteStudentAsync(id);
            if (!success)
            {
                ModelState.AddModelError(string.Empty, "Failed to delete student.");
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
