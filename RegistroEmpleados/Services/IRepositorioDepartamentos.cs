using RegistroEmpleados.Models;

namespace RegistroEmpleados.Services
{
    public interface IRepositorioDepartamentos
    {
        Task Create(Departamento model);
        Task Edit(Departamento model);
        Task Eliminar(int id);
        Task<IEnumerable<Departamento>> Get();
        Task<Departamento> GetById(int id);
        Task<bool> IsExist(string nombre);
    }
}