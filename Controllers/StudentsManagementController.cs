using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentManagement.Model;
using StudentManagement.Service;

namespace StudentManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsManagementController : ControllerBase
    {

        private readonly IStudentService _studentService;
        public StudentsManagementController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpGet("GetStudents")]
        public async Task<List<Student>>GetStudent()
        {
            return await _studentService.GetStudents();
        }

        [HttpPost]
        public async Task<Student> AddStudent(Student student)
        {
            return await _studentService.AddStudent(student);
        }

        [HttpGet("id")]
        public async Task<Student> GetStudentById(int id)
        {
            var data = await _studentService.GetStudentById(id);
            return data;
        }


    }
}
