using Api.Data;

namespace Api.Controllers
{
    public class ClienteController
    {
        private readonly ApplicationDbContext _context;
        public ClienteController(ApplicationDbContext context)
        {
            _context = context;
        }
    }
}