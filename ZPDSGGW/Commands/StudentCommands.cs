using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZPDSGGW.Database;
using ZPDSGGW.Models;
using ZPDSGGW.Repository;

namespace ZPDSGGW.Commands
{
    public class StudentCommands : IRepositoryStudent
    {
        private readonly ZPDSGGWContext _context;

        public StudentCommands(ZPDSGGWContext context)
        {
            _context = context;
        }
        public void CreateStudent(Student student)
        {
            if (student == null)
                throw new ArgumentNullException(nameof(student));
            _context.Add(student);
        }

        public void DeleteStudent(Student student)
        {
            if (student == null)
                throw new ArgumentNullException(nameof(student));
            _context.Student.Remove(student);
        }

        public IEnumerable<Student> GetAllStudents() => _context.Student.ToList();

        public Student GetStudentById(Guid id) => _context.Student.FirstOrDefault(x => x.Id == id);

        public bool SaveChanges() => (_context.SaveChanges() >= 0);

        public void UpdateStudent(Student student)
        {
            //nothing
        }
    }
}
