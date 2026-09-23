using StudentManagement.Model;

namespace StudentManagement.Repository
{
    public interface IStudentRepository
    {
        public Task<List<Student>> GetStudents();
        public Task<Student> GetStudentById(int id);
        public Task<Student> AddStudent(Student student);
    }
}
