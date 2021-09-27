using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Api.Modelos.Dto;
using Api.Data;
using AutoMapper;
using Api.Modelos;
using Microsoft.EntityFrameworkCore;

namespace Api.Repositorio
{
    public class ClienteRepositorio : IClienteRepositorio
    {
        private readonly ApplicationDbContext _context;
        private IMapper _mapper;
        public ClienteRepositorio(ApplicationDbContext context , IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<ClienteDto> CreateUpdate(ClienteDto cliente)
        {
            Cliente cli = _mapper.Map<ClienteDto , Cliente>(cliente);
            if(cliente.Id>0)
            {
                _context.Clientes.Update(cli);
            }
            else
            {
                await _context.Clientes.AddAsync(cli);
            }
            await _context.SaveChangesAsync();
            return _mapper.Map<Cliente,ClienteDto>(cli);
        }

        public async Task<bool> DeleteCliente(int id)
        {
           try
           {
               Cliente cliente = await _context.Clientes.FindAsync(id);
               if(cliente == null)
               {
                   return false;
               }
               _context.Clientes.Remove(cliente);
               await _context.SaveChangesAsync();
               return true;
           }
           catch(Exception)
           {
               return false;
           }

        }

        public async Task<ClienteDto> GetClienteById(int id)
        {
            Cliente Cliente = await _context.Clientes.FindAsync(id);
            return _mapper.Map<ClienteDto>(Cliente);
        }

        public async Task<List<ClienteDto>> GetClientes()
        {
            List<Cliente> lista = await _context.Clientes.ToListAsync();
            return _mapper.Map<List<ClienteDto>>(lista);
        }
    }
}