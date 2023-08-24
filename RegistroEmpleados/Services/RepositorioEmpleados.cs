using Dapper;
using Microsoft.Data.SqlClient;
using RegistroEmpleados.Models;

namespace RegistroEmpleados.Services
{
    public class RepositorioEmpleados : IRepositorioEmpleados
    {
        private readonly IConfiguration configuration;
        private readonly ILogger<RepositorioEmpleados> logger;
        private readonly string ConnectionString;

        public RepositorioEmpleados(IConfiguration configuration,
            ILogger<RepositorioEmpleados> logger)
        {
            
            this.configuration = configuration;
            this.logger = logger;
            ConnectionString = configuration.GetConnectionString("DefaultConnection");

        }

        public async Task Crear(Empleado model)
        {

            using var connection = new SqlConnection(ConnectionString);

            var id = await connection.QuerySingleAsync<int>(
                @"INSERT INTO Empleados
                (Nombre, Edad, Sexo, Email, Telefono, DepartamentoId)
                VALUES
                (@Nombre, @Edad, @Sexo, @Email, @Telefono, @DepartamentoId);

                SELECT SCOPE_IDENTITY();", model);

            model.Id = id;



        }

        public async Task<IEnumerable<DepartamentoViewModel>> Obtener()
        {

            using var connection = new SqlConnection(ConnectionString);

            return await connection.QueryAsync<DepartamentoViewModel>(
                @"SELECT 
	                Id, 
	                Nombre, 
	                Edad, 
	                Sexo, 
	                Email, 
	                Telefono, 
	                DepartamentoId 
                FROM Empleados");


        }

        public async Task<DepartamentoViewModel> ObtenerEmpleado(int id)
        {

            using var connection = new SqlConnection(ConnectionString);

            return await connection.QuerySingleAsync<DepartamentoViewModel>(
                @"SELECT
	                emp.Id,
	                emp.Nombre,
	                emp.Edad,
	                emp.Sexo,
	                emp.Email,
	                emp.Telefono,
	                dpt.Nombre AS Departamento
                FROM Empleados AS emp
                INNER JOIN Departamentos AS dpt
                ON emp.DepartamentoId = dpt.Id
                WHERE emp.Id = @Id", new { id });

        }

        public async Task Actualizar(DepartamentoViewModel model)
        {

            using var connection = new SqlConnection(ConnectionString);

            await connection.ExecuteAsync(
                @"UPDATE Empleados
                SET Nombre = @Nombre,
	                Edad = @Edad,
	                Sexo = @Sexo,
	                Email = @Email,
	                Telefono = @Telefono,
	                DepartamentoId = @DepartamentoId
                WHERE Id = @Id;", model);

        }

        public async Task Eliminar(int id)
        {

            using var connection = new SqlConnection(ConnectionString);

            await connection.ExecuteAsync(
               "DELETE FROM Empleados WHERE Id = @Id;", new { id });

        }

        public async Task<Empleado> ObtenerPorId(int id)
        {

            using var connection = new SqlConnection(ConnectionString);

            return await connection.QueryFirstOrDefaultAsync<Empleado>(
                @"SELECT 
	                Id, 
	                Nombre, 
	                Edad, 
	                Sexo, 
	                Email, 
	                Telefono, 
	                DepartamentoId 
                FROM Empleados
                WHERE Id = @Id", new { id });


        }

    }
}
