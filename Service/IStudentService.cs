using StudentManagement.Model;

namespace StudentManagement.Service
{
    public interface IStudentService
    {
        public Task<List<Student>> GetStudents();
        public Task<Student> GetStudentById(int id);
        public Task<Student> AddStudent(Student student);

        public Task DeleteStudent(int id);
        
        public Task<Student> UpdateStudent(int id, Student student);
    }
}
