using System.ComponentModel.DataAnnotations;

namespace RegistroEmpleados.Models
{
    public class Departamento
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El {0} es obligatorio")]
        [StringLength(maximumLength: 50, MinimumLength = 2, ErrorMessage =
           "La cantidad de letras debe ser entre 3 y 50")]
        public string Nombre { get; set; }
    }
}
