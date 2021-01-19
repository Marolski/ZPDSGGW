using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZPDSGGW.Models;

namespace ZPDSGGW.Repository
{
    public interface IRepositoryStudent
    {
        void CreateStudent(Student student);
        IEnumerable<Student> GetAllStudents();
        Student GetStudentById(Guid id);
        void UpdateStudent(Student student);
        void DeleteStudent(Student student);
        bool SaveChanges();
    }
}
