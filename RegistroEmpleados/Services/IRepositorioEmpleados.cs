using RegistroEmpleados.Models;

namespace RegistroEmpleados.Services
{
    public interface IRepositorioEmpleados
    {
        Task Actualizar(DepartamentoViewModel model);
        Task Crear(Empleado model);
        Task Eliminar(int id);
        Task<IEnumerable<DepartamentoViewModel>> Obtener();
        Task<DepartamentoViewModel> ObtenerEmpleado(int id);
        Task<Empleado> ObtenerPorId(int id);
    }
}