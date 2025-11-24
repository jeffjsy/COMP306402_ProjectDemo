using EnrollmentPortal.Services;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace EnrollmentPortal.Controllers
{
    // UI spelling: Enrolments, API/DTO spelling: Enrollment
    public class EnrolmentsController : Controller
    {
        private readonly SchoolApiClient _apiClient;

        public EnrolmentsController(SchoolApiClient apiClient)
        {
            _apiClient = apiClient;
        }

        // GET: /Enrolments
        // Uses GET (GetAll)
        public async Task<IActionResult> Index()
        {
            var enrolments = await _apiClient.GetAllEnrollmentsAsync();
            return View(enrolments);
        }

        // GET: /Enrolments/Details/5
        // Uses GET (GetById)
        public async Task<IActionResult> Details(int id)
        {
            var enrolment = await _apiClient.GetEnrollmentByIdAsync(id);
            if (enrolment == null)
            {
                return NotFound();
            }

            return View(enrolment);
        }

        // GET: /Enrolments/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: /Enrolments/Create
        // Uses POST – Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EnrollmentCreateDTO model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var created = await _apiClient.CreateEnrollmentAsync(model);
            if (created == null)
            {
                ModelState.AddModelError(string.Empty, "Failed to create enrolment.");
                return View(model);
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: /Enrolments/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var enrolment = await _apiClient.GetEnrollmentByIdAsync(id);
            if (enrolment == null)
            {
                return NotFound();
            }

            var updateModel = new EnrollmentUpdateDTO
            {
                EnrollmentId = enrolment.EnrollmentId,
                EnrollmentDate = enrolment.EnrollmentDate,
                Status = enrolment.Status,
                StudentId = enrolment.StudentId,
                ProgramId = enrolment.ProgramId
            };

            ViewBag.EnrolmentId = id;
            return View(updateModel);
        }

        // POST: /Enrolments/Edit/5
        // Uses PUT – Replace
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, EnrollmentUpdateDTO model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var success = await _apiClient.UpdateEnrollmentAsync(id, model);
            if (!success)
            {
                ModelState.AddModelError(string.Empty, "Failed to update enrolment.");
                return View(model);
            }

            return RedirectToAction(nameof(Details), new { id });
        }

        // GET: /Enrolments/PatchStatus/5
        public async Task<IActionResult> PatchStatus(int id)
        {
            var enrolment = await _apiClient.GetEnrollmentByIdAsync(id);
            if (enrolment == null)
            {
                return NotFound();
            }

            ViewBag.EnrolmentId = id;
            ViewBag.CurrentStatus = enrolment.Status;
            return View();
        }

        // POST: /Enrolments/PatchStatus/5
        // Uses PATCH – partial update (Status only)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PatchStatus(int id, string newStatus)
        {
            if (string.IsNullOrWhiteSpace(newStatus))
            {
                ModelState.AddModelError(string.Empty, "Status cannot be empty.");
                ViewBag.EnrolmentId = id;
                return View();
            }

            using var patchJson = JsonDocument.Parse($"{{\"status\":\"{newStatus}\"}}");
            var success = await _apiClient.PatchEnrollmentAsync(id, patchJson);
            if (!success)
            {
                ModelState.AddModelError(string.Empty, "Failed to patch enrolment status.");
                ViewBag.EnrolmentId = id;
                return View();
            }

            return RedirectToAction(nameof(Details), new { id });
        }

        // POST: /Enrolments/Delete/5
        // Uses DELETE – Remove
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _apiClient.DeleteEnrollmentAsync(id);
            if (!success)
            {
                ModelState.AddModelError(string.Empty, "Failed to delete enrolment.");
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
