using StudentManagement.Model;
using StudentManagement.Repository;

namespace StudentManagement.Service
{
    public class StudentService:IStudentService
    {
        private readonly IStudentRepository _studentRepository;
        public StudentService(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public async Task<List<Student>> GetStudents()
        {
            var data=await _studentRepository.GetStudents();
            return data;
        }
        public async Task<Student> GetStudentById(int id)
        {
            var data = await _studentRepository.GetStudentById(id);
            return data;
        }
        public async Task<Student> AddStudent(Student student)
        {
            var data = await _studentRepository.AddStudent(student);


            return data;
        }

        public async Task DeleteStudent(int id)
        {
            await _studentRepository.DeleteStudent(id);
        }

        public async Task<Student> UpdateStudent(int id, Student student)
        {
            var data = await _studentRepository.UpdateStudent(id, student);
            return data;
        }

    }
}
