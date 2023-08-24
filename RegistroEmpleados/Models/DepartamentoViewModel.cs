using Microsoft.AspNetCore.Mvc.Rendering;

namespace RegistroEmpleados.Models
{
    public class DepartamentoViewModel : Empleado
    {

        public IEnumerable<SelectListItem> Departamentos { get; set; }

        public string Departamento { get; set; }

    }
}
