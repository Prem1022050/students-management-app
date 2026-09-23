using StudentManagement.DbContextFolder;
using StudentManagement.Model;
using Microsoft.EntityFrameworkCore;

namespace StudentManagement.Repository
{
    public class StudentRepository:IStudentRepository
    {
        private readonly MyDbConext _context;
        public StudentRepository(MyDbConext context)
        {
            _context=context;
        }

        public async Task<List<Student>> GetStudents()
        {
            var data = await _context.StudentManagement.ToListAsync();
            return data;
        }

        public async Task<Student> GetStudentById(int id)
        {
            return await _context.StudentManagement.FindAsync(id);
        }
        public async Task<Student> AddStudent(Student student)
        {
            _context.StudentManagement.Add(student);
            await _context.SaveChangesAsync();
            return student;
        }
   
  
    }
}
