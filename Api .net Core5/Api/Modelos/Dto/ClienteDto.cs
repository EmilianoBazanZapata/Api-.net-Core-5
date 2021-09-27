using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Api.Modelos.Dto
{
    public class ClienteDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
    }
}