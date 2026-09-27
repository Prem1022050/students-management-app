using Microsoft.AspNetCore.Mvc;
using StudentManagementMvc.Models;

namespace StudentManagementMvc.Controllers
{
    public class StudentsController : Controller
    {
        private readonly HttpClient _httpClient;

        public StudentsController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("StudentAPI");
        }

        // GET: Students
        public async Task<IActionResult> Index()
        {
            var students = await _httpClient.GetFromJsonAsync<List<Student>>(
                "api/StudentsManagement");

            return View(students);
        }

        public async Task<IActionResult> Image(string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName))
            {
                return NotFound();
            }

            var response = await _httpClient.GetAsync(
                $"api/StudentsManagement/GetStudentImage/{Uri.EscapeDataString(fileName)}");

            if (!response.IsSuccessStatusCode)
            {
                return NotFound();
            }

            var contentType =
                response.Content.Headers.ContentType?.ToString()
                ?? "application/octet-stream";

            var bytes = await response.Content.ReadAsByteArrayAsync();

            return File(bytes, contentType);
        }


        // GET: Students/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var student = await _httpClient.GetFromJsonAsync<Student>(
                $"api/StudentsManagement/{id}");

            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // GET: Students/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Student model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            using var form = new MultipartFormDataContent();

            form.Add(
                new StringContent(model.name),
                "name");

            form.Add(
                new StringContent(model.email),
                "email");

            form.Add(
                new StringContent(model.age.ToString()),
                "age");

            if (model.image != null && model.image.Length > 0)
            {
                var streamContent =
                    new StreamContent(model.image.OpenReadStream());

                streamContent.Headers.ContentType =
                    new System.Net.Http.Headers.MediaTypeHeaderValue(
                        model.image.ContentType);

                form.Add(
                    streamContent,
                    "image",
                    model.image.FileName);
            }

            var response = await _httpClient.PostAsync(
                "api/StudentsManagement/AddStudentWithImage",
                form);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }

            ModelState.AddModelError(
                "",
                "Unable to create student.");

            return View(model);
        }
        // GET: Students/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var student = await _httpClient.GetFromJsonAsync<Student>(
                $"api/StudentsManagement/{id}");

            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(
     int id,
     Student model)
        {
            if (id != model.id)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            using var form =
                new MultipartFormDataContent();

            form.Add(
                new StringContent(model.name),
                "name");

            form.Add(
                new StringContent(model.email),
                "email");

            form.Add(
                new StringContent(model.age.ToString()),
                "age");

            if (model.image != null &&
                model.image.Length > 0)
            {
                var streamContent =
                    new StreamContent(
                        model.image.OpenReadStream());

                streamContent.Headers.ContentType =
                    new System.Net.Http.Headers
                        .MediaTypeHeaderValue(
                            model.image.ContentType);

                form.Add(
                    streamContent,
                    "image",
                    model.image.FileName);
            }

            var response =
                await _httpClient.PutAsync(
                    $"api/StudentsManagement/{id}/UpdateWithImage",
                    form);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(
                    nameof(Index));
            }

            ModelState.AddModelError(
                "",
                "Unable to update student.");

            return View(model);
        }
        // GET: Students/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var student = await _httpClient.GetFromJsonAsync<Student>(
                $"api/StudentsManagement/{id}");

            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var response = await _httpClient.DeleteAsync(
                $"api/StudentsManagement/{id}");

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }

            return Problem("Unable to delete student.");
        }
    }
}
