using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using ZPDSGGW.Enums;
using ZPDSGGW.Models;
using ZPDSGGW.Repository;

namespace ZPDSGGW.Authentication
{
    public class AuthenticationManager : IAuthenticationManager
    {
        private List<User> _users = new List<User>
        {
            new User{Id = Guid.NewGuid(), Name= "Marcin", Surname="Brzoska", Username="test", Password="1qaz@WSX", Role=Roles.Student},
            new User{Id = Guid.NewGuid(), Name= "Andrzej", Surname="Ogarniacz", Username="test2", Password="1qaz@WSX", Role=Roles.Promoter}
        };
        private readonly Key _key;
        private readonly IRepositoryUser _repository;

        public AuthenticationManager(IOptions<Key> key, IRepositoryUser repository)
        {
            _key = key.Value;
            _repository = repository;
        }
        public User Authenticate(string username, string password)
        {
            var user = _repository.GetUserByCredential(username, password);

            if (user == null)
                return null;

            //generate JWT token
            var tokenHandler = new JwtSecurityTokenHandler();
            var secretKey = Encoding.ASCII.GetBytes(_key.SecretKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Name),
                    new Claim(ClaimTypes.Role, user.Role)
                }),
                Expires = DateTime.UtcNow.AddMinutes(60),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKey), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.Token = tokenHandler.WriteToken(token);

            return user.WithoutPassword();
        }
    }
}
