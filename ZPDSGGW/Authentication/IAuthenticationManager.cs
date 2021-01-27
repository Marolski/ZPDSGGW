using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZPDSGGW.Models;

namespace ZPDSGGW.Authentication
{
    public interface IAuthenticationManager
    {
        User Authenticate(string username, string password);
    }
}
