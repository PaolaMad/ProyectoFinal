using AutoMapper;
using RegistroEmpleados.Models;

namespace RegistroEmpleados.Services
{
    public class AppMapperProfiles : Profile
    {

        public AppMapperProfiles()
        {

            CreateMap<Empleado, DepartamentoViewModel>();

        }

    }
}
