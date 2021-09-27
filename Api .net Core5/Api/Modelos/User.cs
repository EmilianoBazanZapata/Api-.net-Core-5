using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Api.Modelos
{
    public class User
    {
        [Key]
        public int  id { get; set; }
        public string  UserName { get; set; }
        public byte[]  PasswordHash { get; set; }
        public byte[]  PasswordSalt { get; set; }
    }
}