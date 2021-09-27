using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Data;
using Api.Modelos;
using Api.Modelos.Dto;
using Api.Repositorio;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        //private readonly ApplicationDbContext _context;
        private readonly IClienteRepositorio _ClienteRepositorio;
        protected ResponseDto _response;
        public ClienteController(IClienteRepositorio ClienteRepositorio)
        {
            _ClienteRepositorio = ClienteRepositorio;
            _response = new ResponseDto();
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cliente>>> GetClientes()
        {
            //return await _context.Clientes.ToListAsync();
            try
            {
                var lista = await _ClienteRepositorio.GetClientes();
                _response.Result = lista;
                _response.DisplayMessage = "Lista de Clientes";
            }
            catch (Exception ex)
            {
                _response.IsSucces = false;
                _response.ErrorMessages = new List<string> { ex.ToString() };
            }
            return Ok(_response);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Cliente>> GetCliente(int id)
        {
            /*var cliente = await _context.Clientes.FindAsync(id);
            if (cliente == null)
            {
                return NotFound();
            }
            return cliente;*/
            var cliente = await _ClienteRepositorio.GetClienteById(id);
            if (cliente == null)
            {
                _response.IsSucces = false;
                _response.DisplayMessage = "el cliente no existe";
                return NotFound(_response);
            }
            _response.Result = cliente;
            _response.DisplayMessage = "informacion del cliente";
            return Ok(_response);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> PutCliente(int id, ClienteDto cliente)
        {
            /*if (id != cliente.Id)
            {
                return BadRequest();
            }

            _context.Entry(cliente).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
            return NoContent();*/
            try
            {
                ClienteDto model = await _ClienteRepositorio.CreateUpdate(cliente);
                _response.Result = model;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSucces = false;
                _response.DisplayMessage = "ocurrio un problema";
                _response.ErrorMessages = new List<string> { ex.ToString() };
                return BadRequest(_response);
            }
        }
        [HttpPost]
        public async Task<ActionResult<Cliente>> PostCliente(ClienteDto cliente)
        {
            /*_context.Clientes.Add(cliente);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetCliente", new {id  = cliente.Id },cliente);*/
            try
            {
                ClienteDto model = await _ClienteRepositorio.CreateUpdate(cliente);
                _response.Result = model;
                return CreatedAtAction("GetCliente", new { id = model.Id }, _response);
            }
            catch (Exception ex)
            {
                _response.IsSucces = false;
                _response.DisplayMessage = "ocurrio un problema";
                _response.ErrorMessages = new List<string> { ex.ToString() };
                return BadRequest(_response);
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCliente(int id)
        {
            /*var cliente = await _context.Clientes.FindAsync(id);
            if(cliente == null)
            {
                return NoContent();
            }
            _context.Clientes.Remove(cliente);
            await _context.SaveChangesAsync();
            return NoContent();*/
            try
            {
                bool estaEliminado = await _ClienteRepositorio.DeleteCliente(id);
                if (estaEliminado)
                {
                    _response.Result = estaEliminado;
                    _response.DisplayMessage = "Cliente Eliminado con Exito";
                    return Ok(_response);
                }
                else
                {
                    _response.IsSucces = false;
                    _response.DisplayMessage = "error al eliminar el cliente";
                    return BadRequest(_response);
                }
            }
            catch (Exception ex)
            {
                _response.IsSucces = false;
                _response.DisplayMessage = "ocurrio un problema";
                _response.ErrorMessages = new List<string> { ex.ToString() };
                return BadRequest(_response);
            }
        }

    }
}