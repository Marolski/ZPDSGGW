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
        IEnumerable<User> GetAllUsers(Roles role);
        User GetUserById(Guid id);
        void UpdateUser(User user);
        void DeleteUser(User user);
        bool SaveChanges();
    }
}
