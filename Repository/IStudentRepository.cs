using StudentManagement.Model;

namespace StudentManagement.Repository
{
    public interface IStudentRepository
    {
        public Task<List<Student>> GetStudents();
        public Task<Student> GetStudentById(int id);
        public Task<Student> AddStudent(Student student);
        public Task<Student> DeleteStudent(int id);
        public Task<Student> UpdateStudent(int id,Student student);
        
    }
}
