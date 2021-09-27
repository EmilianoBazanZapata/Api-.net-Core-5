using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Api.Modelos;
using Api.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace Api.Repositorio
{
    public class UserReposirtorio : IUserRepositorio
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;
        public UserReposirtorio(ApplicationDbContext context , IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }
        public async Task<string> Login(string UserName, string Password)
        {
             var user = await _context.Users.FirstOrDefaultAsync(
                x => x.UserName.ToLower().Equals(UserName.ToLower()));

            if (user == null)
            {
                return "nouser";
            }
            else if(!VerificarPasswordHash(Password, user.PasswordHash, user.PasswordSalt))
            {
                return "wrongpassword";
            }
            else
            {
                return CrearToken(user);
            }
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
        public bool VerificarPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != passwordHash[i])
                    {
                        return false;
                    }
                }
                return true;
            }
        }

        private string CrearToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.id.ToString()),
                new Claim(ClaimTypes.Name , user.UserName)
            };
            var key  = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value));

            var creds  = new SigningCredentials(key,SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = System.DateTime.Now.AddDays(1),
                SigningCredentials  =creds
            };
            
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}