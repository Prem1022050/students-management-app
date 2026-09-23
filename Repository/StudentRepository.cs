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

        public async Task DeleteStudent(int id) {
            
           var data= _context.StudentManagement.Remove(await _context.StudentManagement.FindAsync(id));
           
        }
        
        public async Task<Student> UpdateStudent(int id, Student student)
        {
            var existingStudent = await _context.StudentManagement.FindAsync(id);
            if (existingStudent == null)
            {
                return null;
            }
            existingStudent.name = student.name;
            existingStudent.age = student.age;
            existingStudent.email = student.email;
            await _context.SaveChangesAsync();
            return existingStudent;
        }

       

    }
}
