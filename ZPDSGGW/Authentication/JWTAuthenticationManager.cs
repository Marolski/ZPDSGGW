using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using ZPDSGGW.Models;
using ZPDSGGW.Repository;

namespace ZPDSGGW.Commands
{
    public class JWTAuthenticationManager : IJWTAuthenticationManager
    {
        private readonly IList<User> users = new List<User>
        {
            new User{Id = new Guid("4ae1cbae-787f-42d9-b4a4-16245db37d5b"), Username = "student", Password= "1qaz@WSX", Role= Enums.Roles.Student, Name = "Marcin", Surname= "Student" },
            new User{Id = new Guid("8f351283-d426-4e98-b3b3-529946c70b79"), Username = "promotor", Password= "1qaz@WSX", Role= Enums.Roles.Promoter, Name = "Kacper", Surname= "Promotor"},
            new User{Id = new Guid("ca8e0fcd-f5ed-4e85-a18a-6d4d86a97883"), Username = "dziekanat", Password= "1qaz@WSX", Role= Enums.Roles.Deanery, Name = "Janina", Surname= "Baba"},
        };
        private readonly string key;

        public JWTAuthenticationManager(string key)
        {
            this.key = key;
        }
        public string Authentication(string name, string password)
        {
            if (!users.Any(users => users.Username == name && users.Password == password))
                return null;
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.ASCII.GetBytes(key);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, name)
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey),
                SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);  
        }
    }
}
