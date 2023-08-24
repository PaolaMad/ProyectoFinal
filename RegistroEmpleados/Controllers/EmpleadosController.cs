using Microsoft.AspNetCore.Mvc;
using Dapper;
using Microsoft.AspNetCore.Mvc.Rendering;
using RegistroEmpleados.Services;
using RegistroEmpleados.Models;
using System.Reflection;
using AutoMapper;
using Microsoft.Data.SqlClient;

namespace RegistroEmpleados.Controllers
{
    public class EmpleadosController : Controller
    {
        private readonly IRepositorioEmpleados repositorioEmpleados;
        private readonly IRepositorioDepartamentos repositorioDepartamentos;
        private readonly IMapper mapper;

        public EmpleadosController(IRepositorioEmpleados repositorioEmpleados,
            IRepositorioDepartamentos repositorioDepartamentos,
            IMapper mapper)
        {
            this.repositorioEmpleados = repositorioEmpleados;
            this.repositorioDepartamentos = repositorioDepartamentos;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Crear()
        {

            var model = new DepartamentoViewModel();

            model.Departamentos = await ObtenerDepartamentos();

            return View(model);

        }

        [HttpPost]
        public async Task<IActionResult> Crear(DepartamentoViewModel model)
        {

            if (!ModelState.IsValid)
            {

                model.Departamentos = await ObtenerDepartamentos();

                return View(model);
            }

            await repositorioEmpleados.Crear(model);

            return RedirectToAction("Lista");

        }

        [HttpGet]
        public async Task<IActionResult> Editar(int id)
        {

            var empleado = await repositorioEmpleados.ObtenerPorId(id);

            if (empleado is null)
            {

                return RedirectToAction("NotFound", "Home");

            }

            var model = mapper.Map<DepartamentoViewModel>(empleado);

            model.Departamentos = await ObtenerDepartamentos();

            return View(model);

        }

        [HttpPost]
        public async Task<IActionResult> Editar(DepartamentoViewModel model)
        {

            if (!ModelState.IsValid)
            {

                return View(model);

            }

            var empleado = await repositorioEmpleados.ObtenerPorId(model.Id);

            if(empleado is null)
            {

                return RedirectToAction("NotFound", "Home");

            }

            var dpt = await repositorioDepartamentos.GetById(model.DepartamentoId);

            if (dpt is null)
            {

                return RedirectToAction("NotFound", "Home");

            }

            await repositorioEmpleados.Actualizar(model);

            return RedirectToAction("Lista");

        }

        [HttpGet]
        public async Task<IActionResult> Eliminar(int id)
        {

            var empleado = await repositorioEmpleados.ObtenerPorId(id);

            if (empleado is null)
            {

                return RedirectToAction("NotFound", "Home");

            }

            return View(empleado);

        }

        [HttpPost]
        public async Task<IActionResult> EliminarEmpleado(int id)
        {

            var empleado = await repositorioEmpleados.ObtenerPorId(id);

            if (empleado is null)
            {

                return RedirectToAction("NotFound", "Home");

            }

            await repositorioEmpleados.Eliminar(id);

            return RedirectToAction("Lista");

        }

        public async Task<IActionResult> Lista()
        {

            var empleados = await repositorioEmpleados.Obtener();

            return View(empleados);

        }

        public async Task<IActionResult> EmpleadoInd(DepartamentoViewModel model)
        {

            var empleado = await repositorioEmpleados.ObtenerPorId(model.Id);

            if (empleado is null)
            {

                return RedirectToAction("NotFound", "Home");

            }

            var empleadoConDepartamento = await repositorioEmpleados.ObtenerEmpleado(model.Id);

            return View(empleadoConDepartamento);

        }

        

        private async Task<IEnumerable<SelectListItem>> ObtenerDepartamentos()
        {

            var dpts = await repositorioDepartamentos.Get();

            return dpts.Select(x => new SelectListItem(x.Nombre, x.Id.ToString()))
                .Prepend(new SelectListItem("Seleccionar un Departamento", ""));

        }

    }

    

}
