using System.ComponentModel.DataAnnotations.Schema;

namespace Day3.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }

        public Adress Adress { get; set; }

        public List<Course> Courses { get; set; }
    }
}
