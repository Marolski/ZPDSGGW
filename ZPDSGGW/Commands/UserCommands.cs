using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZPDSGGW.Database;
using ZPDSGGW.Enums;
using ZPDSGGW.Models;
using ZPDSGGW.Repository;

namespace ZPDSGGW.Commands
{
    public class UserCommands : IRepositoryUser
    {
        private readonly ZPDSGGWContext _context;

        public UserCommands(ZPDSGGWContext context)
        {
            _context = context;
        }
        public void CreateUser(User student)
        {
            if (student == null)
                throw new ArgumentNullException(nameof(student));
            _context.Add(student);
        }

        public void DeleteUser(User student)
        {
            if (student == null)
                throw new ArgumentNullException(nameof(student));
            _context.User.Remove(student);
        }

        public IEnumerable<User> GetAllUsers(string role)
        {
           return _context.User.Where(x => x.Role ==  role).ToList();
        }

        public User GetUserByCredential(string username, string password)
        {
            return _context.User.FirstOrDefault(x => x.Username == username && x.Password == password);
        }

        public User GetUserById(Guid id) => _context.User.FirstOrDefault(x => x.Id == id);

        public bool SaveChanges() => (_context.SaveChanges() >= 0);

        public void UpdateUser(User student)
        {
            //nothing
        }
    }
}
