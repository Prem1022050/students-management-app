using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentManagement.AzureStorage;
using StudentManagement.Model;
using StudentManagement.Service;

namespace StudentManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsManagementController : ControllerBase
    {

        private readonly IStudentService _studentService;
        private readonly BlobService _blobService;
        public StudentsManagementController(IStudentService studentService, BlobService blobService)
        {
            _studentService = studentService;
            _blobService = blobService;
        }

        [HttpGet]
        public async Task<List<Student>> GetStudent()
        {
            return await _studentService.GetStudents();
        }

        //[HttpPost]
        //public async Task<Student> AddStudent(Student student)
        //{
        //    return await _studentService.AddStudent(student);
        //}

        [HttpPost("AddStudentWithImage")]
        public async Task<IActionResult> AddStudentWithImage(
    [FromForm] string name,
    [FromForm] string email,
    [FromForm] int age,
    IFormFile? image)
        {
            string? imageFileName = null;

            if (image != null && image.Length > 0)
            {
                if (image.Length > 2 * 1024 * 1024)
                {
                    return BadRequest(
                        "File size cannot exceed 2 MB.");
                }

                var allowedTypes = new[]
                {
            "image/jpeg",
            "image/png"
        };

                if (!allowedTypes.Contains(image.ContentType))
                {
                    return BadRequest(
                        "Only JPG and PNG images are allowed.");
                }

                imageFileName =
                    await _blobService.UploadFileAsync(image);
            }

            var student = new Student
            {
                name = name,
                email = email,
                age = age,
                profileImageUrl = imageFileName
            };

            var result =
                await _studentService.AddStudent(student);

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<Student> GetStudentById(int id)
        {
            var data = await _studentService.GetStudentById(id);
            return data;
        }

        [HttpDelete("{id}")]
        public async Task DeleteStudent(int id)
        {
            await _studentService.DeleteStudent(id);
        }

        [HttpPut("{id}")]
        public async Task<Student> UpdateStudent(int id, Student student)
        {
            return await _studentService.UpdateStudent(id, student);

        }

        [HttpGet("GetStudentImage/{fileName}")]
        public async Task<IActionResult> GetStudentImage(string fileName)
        {
            var result = await _blobService.GetFileAsync(fileName);

            if (result == null)
            {
                return NotFound();
            }

            return File(
                result.Value.Stream,
                result.Value.ContentType);
        }

        //    [HttpPost("UploadImage")]
        //    public async Task<IActionResult> UploadImage(IFormFile file)
        //    {
        //        if (file == null || file.Length == 0)
        //        {
        //            return BadRequest("Please select a file.");
        //        }
        //        if (file.Length > 2 * 1024 * 1024)
        //        {
        //            return BadRequest("File size cannot exceed 2 MB.");
        //        }

        //        var allowedTypes = new[]
        //        {
        //    "image/jpeg",
        //    "image/png"
        //};

        //        if (!allowedTypes.Contains(file.ContentType))
        //        {
        //            return BadRequest("Only JPG and PNG images are allowed.");
        //        }
        //        var imageUrl = await _blobService.UploadFileAsync(file);

        //        return Ok(new
        //        {
        //            message = "File uploaded successfully",
        //            url = imageUrl
        //        });
        //    }
    }
}
