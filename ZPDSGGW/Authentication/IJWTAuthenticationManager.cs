using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ZPDSGGW.Repository
{
    public interface IJWTAuthenticationManager
    {
        string Authentication(string name, string password);
    }
}
