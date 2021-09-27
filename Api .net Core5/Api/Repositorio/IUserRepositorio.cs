using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Api.Modelos;

namespace Api.Repositorio
{
    public interface IUserRepositorio
    {
        Task<int> Register(User user,string password);
        Task<string> Login(string UserName, string Password);
         Task<bool> UserExists(string UserName);
    }
}