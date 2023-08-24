using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RegistroEmpleados.Models;
using RegistroEmpleados.Services;
using System.Diagnostics;

namespace RegistroEmpleados.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IRepositorioEmpleados repositorioEmpleados;
        private readonly IRepositorioDepartamentos repositorioDepartamentos;

        public HomeController(ILogger<HomeController> logger, 
            IRepositorioEmpleados repositorioEmpleados,
            IRepositorioDepartamentos repositorioDepartamentos)
        {
            _logger = logger;
            this.repositorioEmpleados = repositorioEmpleados;
            this.repositorioDepartamentos = repositorioDepartamentos;
        }

        public async Task<IActionResult> Index()
        {

            var model = new DepartamentoViewModel();

            model.Departamentos = await ObtenerDepartamentos();

            return View(model);

        }

        [HttpPost]
        public async Task<IActionResult> Index(DepartamentoViewModel model)
        {

            if (!ModelState.IsValid)
            {

                model.Departamentos = await ObtenerDepartamentos();

                return View(model);
            }

            await repositorioEmpleados.Crear(model);

            return RedirectToAction("Lista", "Empleados");

        }

        private async Task<IEnumerable<SelectListItem>> ObtenerDepartamentos()
        {

            var dpts = await repositorioDepartamentos.Get();

            return dpts.Select(x => new SelectListItem(x.Nombre, x.Id.ToString()))
                .Prepend(new SelectListItem("Seleccionar un Departamento", ""));

        }

        public IActionResult NotFound()
        {

            return View();

        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}