using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Api.Modelos;
using Api.Data;
using Microsoft.EntityFrameworkCore;

namespace Api.Repositorio
{
    public class UserReposirtorio : IUserRepositorio
    {
        private readonly ApplicationDbContext _context;
        public UserReposirtorio(ApplicationDbContext context)
        {
            _context = context;
        }
        public Task<string> Login(string UserName, string Password)
        {
            throw new NotImplementedException();
        }

        public async Task<int> Register(User user, string password)
        {
            try
            {
                if(await UserExists(user.UserName))
                {
                    return -1;
                }

                CrearPasswordHash(password , out byte[] passwordHash , out byte[] passwordSalt);
                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;

                await _context.Users.AddAsync(user);
                await _context.SaveChangesAsync();
                return user.id;
            }
            catch(Exception)
            {
                return -500;
            }
        }

        public async Task<bool> UserExists(string UserName)
        {
            if(await _context.Users.AnyAsync(x => x.UserName.ToLower().Equals(UserName.ToLower())))
            {
                return true;
            }
            return false;
        }
        private void CrearPasswordHash(string password , out byte[] passwordHash , out byte[] passwordSalt)
        {
            using(var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
    }
}