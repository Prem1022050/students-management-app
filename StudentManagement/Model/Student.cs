namespace StudentManagement.Model
{
    public class Student
    {
        public int id { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public int age { get; set; }

        public string? profileImageUrl { get; set; }    
    }
}
