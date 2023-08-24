using Microsoft.AspNetCore.Mvc;
using RegistroEmpleados.Models;
using RegistroEmpleados.Services;
using System.Reflection;

namespace RegistroEmpleados.Controllers
{
    public class DepartamentosController: Controller
    {
        private readonly IRepositorioDepartamentos repositorioDepartamentos;

        public DepartamentosController(IRepositorioDepartamentos repositorioDepartamentos)
        {
            this.repositorioDepartamentos = repositorioDepartamentos;
        }

        public async Task<IActionResult> CrearDepartamento()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CrearDepartamento(Departamento model)
        {

            if (!ModelState.IsValid)
            {

                return View(model);

            }

            var existDpt = await repositorioDepartamentos.IsExist(model.Nombre);

            if(existDpt)
            {

                ModelState
                    .AddModelError(
                    nameof(model.Nombre), $"El nombre {model.Nombre} ya existe.");

                return View(model);

            }

            await repositorioDepartamentos.Create(model);

            return RedirectToAction("ListaDepartamento");
        }

        public async Task<IActionResult> ListaDepartamento()
        {

            var dpts = await repositorioDepartamentos.Get();

            return View(dpts);

        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {

            var dpt = await repositorioDepartamentos.GetById(id);

            return View(dpt);

        }

        [HttpPost]
        public async Task<IActionResult> Edit(Departamento model)
        {

            var existe = await repositorioDepartamentos.GetById(model.Id);

            if (existe is null)
            {

                return RedirectToAction("NotFound", "Home");

            }

            await repositorioDepartamentos.Edit(model);

            return RedirectToAction("ListaDepartamento");

        } 

        [HttpGet]
        public async Task<IActionResult> Eliminar(int id)
        {

            var dpt = await repositorioDepartamentos.GetById(id);

            if (dpt is null)
            {

                return RedirectToAction("NotFound", "Home");

            }

            return View(dpt);

        }

        [HttpPost]
        public async Task<IActionResult> EliminarDepartamento(int id)
        {

            var empleado = await repositorioDepartamentos.GetById(id);

            if (empleado is null)
            {

                return RedirectToAction("NotFound", "Home");

            }

            await repositorioDepartamentos.Eliminar(id);

            return RedirectToAction("ListaDepartamento");

        }

        public async Task<IActionResult> VerficateExistDpt(string name, Departamento model)
        {
            var dptId = model.Id;

            var existType = await repositorioDepartamentos.IsExist(name);

            if (existType)
            {

                return Json($"El nombre {name} ya existe.");

            }

            return Json(true);

        }

    }
}
