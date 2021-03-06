using Api.Modelos;
using Microsoft.EntityFrameworkCore;

namespace Api.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
        {
            
        }
        public DbSet<Cliente> Clientes{get;set;}
        public DbSet<User> Users{get;set;}
    }
}