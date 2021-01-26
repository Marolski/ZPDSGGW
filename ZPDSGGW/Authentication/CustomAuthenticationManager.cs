using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZPDSGGW.Enums;
using ZPDSGGW.Models;

namespace ZPDSGGW.Authentication
{
    public class CustomAuthenticationManager : ICustomAuthenticationManager
    {

        private readonly IList<User> users = new List<User>
        {
            new User{Id = new Guid("4ae1cbae-787f-42d9-b4a4-16245db37d5b"), Username = "student", Password= "1qaz@WSX", Role= Enums.Roles.Student, Name = "Marcin", Surname= "Student" },
            new User{Id = new Guid("8f351283-d426-4e98-b3b3-529946c70b79"), Username = "promotor", Password= "1qaz@WSX", Role= Enums.Roles.Promoter, Name = "Kacper", Surname= "Promotor"},
            new User{Id = new Guid("ca8e0fcd-f5ed-4e85-a18a-6d4d86a97883"), Username = "dziekanat", Password= "1qaz@WSX", Role= Enums.Roles.Deanery, Name = "Janina", Surname= "Baba"},
        };
        private readonly IDictionary<string, Tuple<string, Roles>> tokens = new Dictionary<string, Tuple<string, Roles>>();
        public IDictionary<string, Tuple<string, Roles>> Tokens => tokens;
        public string Authentication(string name, string password)
        {
            if (!users.Any(users => users.Username == name && users.Password == password))
                return null;

            var token = Guid.NewGuid().ToString();

            tokens.Add(token, new Tuple<string, Roles>(name,users.First(u => u.Username == name && u.Password==password).Role));


            return token;
        }
    }
}
