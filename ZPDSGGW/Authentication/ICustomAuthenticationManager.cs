using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZPDSGGW.Enums;
using ZPDSGGW.Models;

namespace ZPDSGGW.Authentication
{
    public interface ICustomAuthenticationManager
    {
        string Authentication(string name, string password);
        IDictionary<string, Tuple<string, Roles>> Tokens { get; }
    }
}
