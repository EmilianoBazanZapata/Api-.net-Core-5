using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Api.Repositorio;
using Api.Modelos.Dto;
using Api.Modelos;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepositorio _user;
        protected ResponseDto _response;

        public UserController(IUserRepositorio user)
        {
            _user = user;
            _response = new ResponseDto();
        }
        [HttpPost("Register")]
        public async Task<ActionResult> Register(UserDto user)
        {
            var respuesta = await _user.Register(
                    new User
                    {
                        UserName = user.UserName
                    }, user.Password
                );

            if (respuesta == -1)
            {
                _response.IsSucces = false;
                _response.DisplayMessage = "el usuario ya existe";
                return BadRequest(_response);
            }
            if (respuesta == -500)
            {
                _response.IsSucces = false;
                _response.DisplayMessage = "error al crear el usuario";
                return BadRequest(_response);
            }
            _response.DisplayMessage = "usuario creado exitosamente";
            return Ok(_response);
        }
        [HttpPost("Login")]
        public async Task<ActionResult> Login(UserDto user)
        {
            var respuesta = await _user.Login(user.UserName, user.Password);
            if (respuesta == "nouser")
            {
                _response.IsSucces = false;
                _response.DisplayMessage = "el usuario no existe";
                return BadRequest(_response);
            }
            if (respuesta == "wrongpassword")
            {
                _response.IsSucces = false;
                _response.DisplayMessage = "password incorrecta";
                return BadRequest(_response);
            }
            _response.Result = respuesta;
            _response.DisplayMessage="usuario conectado";

            return Ok(_response);
        }
    }
}