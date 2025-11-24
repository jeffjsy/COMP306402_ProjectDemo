using EnrollmentPortal.Services;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace EnrollmentPortal.Controllers
{
    public class ProgramsController : Controller
    {
        private readonly SchoolApiClient _apiClient;

        public ProgramsController(SchoolApiClient apiClient)
        {
            _apiClient = apiClient;
        }

        // GET: /Programs
        // Uses GET (GetAll)
        public async Task<IActionResult> Index()
        {
            var programs = await _apiClient.GetAllProgramsAsync();
            return View(programs);
        }

        // GET: /Programs/Details/5
        // Uses GET (GetById)
        public async Task<IActionResult> Details(int id)
        {
            var program = await _apiClient.GetProgramByIdAsync(id);
            if (program == null)
            {
                return NotFound();
            }

            return View(program);
        }

        // GET: /Programs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: /Programs/Create
        // Uses POST – Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProgramCreateDTO model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var created = await _apiClient.CreateProgramAsync(model);
            if (created == null)
            {
                ModelState.AddModelError(string.Empty, "Failed to create program.");
                return View(model);
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: /Programs/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var program = await _apiClient.GetProgramByIdAsync(id);
            if (program == null)
            {
                return NotFound();
            }

            var updateModel = new ProgramUpdateDTO
            {
                ProgramId = program.ProgramId,
                Name = program.Name,
                Description = program.Description,
                DurationMonths = program.DurationMonths,
                TuitionFee = program.TuitionFee
            };

            ViewBag.ProgramId = id;
            return View(updateModel);
        }

        // POST: /Programs/Edit/5
        // Uses PUT – Replace
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ProgramUpdateDTO model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var success = await _apiClient.UpdateProgramAsync(id, model);
            if (!success)
            {
                ModelState.AddModelError(string.Empty, "Failed to update program.");
                return View(model);
            }

            return RedirectToAction(nameof(Details), new { id });
        }

        // GET: /Programs/PatchDescription/5
        public async Task<IActionResult> PatchDescription(int id)
        {
            var program = await _apiClient.GetProgramByIdAsync(id);
            if (program == null)
            {
                return NotFound();
            }

            ViewBag.ProgramId = id;
            ViewBag.CurrentDescription = program.Description;
            return View();
        }

        // POST: /Programs/PatchDescription/5
        // Uses PATCH – partial update (Description only)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PatchDescription(int id, string newDescription)
        {
            if (string.IsNullOrWhiteSpace(newDescription))
            {
                ModelState.AddModelError(string.Empty, "Description cannot be empty.");
                ViewBag.ProgramId = id;
                return View();
            }

            using var patchJson = JsonDocument.Parse($"{{\"description\":\"{newDescription}\"}}");
            var success = await _apiClient.PatchProgramAsync(id, patchJson);
            if (!success)
            {
                ModelState.AddModelError(string.Empty, "Failed to patch program description.");
                ViewBag.ProgramId = id;
                return View();
            }

            return RedirectToAction(nameof(Details), new { id });
        }

        // POST: /Programs/Delete/5
        // Uses DELETE – Remove
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _apiClient.DeleteProgramAsync(id);
            if (!success)
            {
                ModelState.AddModelError(string.Empty, "Failed to delete program.");
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
