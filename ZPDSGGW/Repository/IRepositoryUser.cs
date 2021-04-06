using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZPDSGGW.Enums;
using ZPDSGGW.Models;

namespace ZPDSGGW.Repository
{
    public interface IRepositoryUser
    {
        void CreateUser(User user);
        IEnumerable<User> GetAllUsers(string role);
        User GetUserById(Guid id);
        void UpdateUser(User user);
        void DeleteUser(User user);
        User GetUserByCredential(string username, string password);
        bool SaveChanges();
    }
}
