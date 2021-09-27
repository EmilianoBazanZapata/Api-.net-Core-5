using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Api.Modelos.Dto
{
    public class ResponseDto
    {
        public bool IsSucces { get; set; } = true;
        public object Result { get; set; }
        public string DisplayMessage {get;set;}
        public List<string> ErrorMessages{get;set;}
    }
}