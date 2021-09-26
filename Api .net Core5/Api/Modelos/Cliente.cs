using System.ComponentModel.DataAnnotations;

namespace Api.Modelos
{
    public class Cliente
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Nombre { get; set; }
        [Required]
        [MaxLength(50)]
        public string Apellido { get; set; }
        [Required]
        [MaxLength(150)]
        public string Direccion { get; set; }
        [Required]
        [MaxLength(100)]
        public string Telefono { get; set; }
    }
}