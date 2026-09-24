using Microsoft.EntityFrameworkCore;
using StudentManagement.Model;

namespace StudentManagement.DbContextFolder
{
    public class MyDbConext:DbContext
    {

        public MyDbConext(DbContextOptions<MyDbConext> options) : base(options)
        {
        }
        public DbSet<Student> StudentManagement { get;set; }
    }
}
