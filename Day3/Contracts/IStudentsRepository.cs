using Day3.Models;

namespace Day3.Contracts
{
    public interface IStudentsRepository
    {
        Task<List<Student>> GetAllstudents();
        Task<Student> GetStudentById(int id);
        Task<Student> AddNewStudent(Student p);

         Task<bool> Delete(int id);

         Task Update(Student s);
    }
}
