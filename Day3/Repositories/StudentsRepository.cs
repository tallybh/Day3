using Day3.Contracts;
using Day3.Models;
using Microsoft.EntityFrameworkCore;

namespace Day3.Repositories
{
    public class StudentsRepository : IStudentsRepository
    {
        private AppDbContext _context;

        public StudentsRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<Student> AddNewStudent(Student s)
        {
            _context.Students.Add(s);
            await _context.SaveChangesAsync();
            return s;
        }

        public async Task<bool> Delete(int id)
        {
            var studentToDelete = _context.Students.ToList().Where(p => p.Id == id).FirstOrDefault();
            if (studentToDelete != null)
            {
                _context.Students.Remove(studentToDelete);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public Task<List<Student>> GetAllstudents()
        {
            return Task.FromResult(_context.Students.ToList());
        }

        public Task<Student> GetStudentById(int id)
        {
            return Task.FromResult(_context.Students.ToList().Where(p => p.Id == id).FirstOrDefault());
        }

        public Task Update(Student s)
        {
            _context.Students.Update(s);
            _context.SaveChangesAsync();
            return Task.CompletedTask;
        }
    }
}
