using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Api.Modelos.Dto;

namespace Api.Repositorio
{
    public interface IClienteRepositorio
    {
        Task<List<ClienteDto>> GetClientes();
        Task<ClienteDto> GetClienteById(int id);
        Task<ClienteDto> CreateUpdate(ClienteDto cliente);
        Task<bool> DeleteCliente(int id);
    }
}