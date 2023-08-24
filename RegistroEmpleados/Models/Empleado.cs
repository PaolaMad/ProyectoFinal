using System.ComponentModel.DataAnnotations;

namespace RegistroEmpleados.Models
{
    public class Empleado
    {

        public int Id { get; set; }

        [Required(ErrorMessage = "El {0} es obligatorio")]
        [StringLength(maximumLength: 50, MinimumLength = 2, ErrorMessage =
           "La cantidad de letras debe ser entre 3 y 50")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "La {0} es obligatoria")]
        public int Edad { get; set; }

        [Required(ErrorMessage = "El {0} es obligatorio")]
        public string Sexo { get; set; }

        [Required(ErrorMessage = "El Correo Electrónico es obligatorio")]
        public string Email { get; set; }

        [Required(ErrorMessage = "El {0} es obligatorio")]
        public int Telefono { get; set; }

        [Required(ErrorMessage = "El {0} es obligatorio")]
        public int DepartamentoId { get; set; }

    }
}
