using Dapper;
using Microsoft.Data.SqlClient;
using RegistroEmpleados.Models;
using System.Reflection;

namespace RegistroEmpleados.Services
{
    public class RepositorioDepartamentos : IRepositorioDepartamentos
    {
        private readonly IConfiguration configuration;
        private readonly ILogger<RepositorioDepartamentos> logger;
        private readonly string ConnectionString;

        public RepositorioDepartamentos(IConfiguration configuration,
            ILogger<RepositorioDepartamentos> logger)
        {
            this.configuration = configuration;
            this.logger = logger;
            ConnectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task Create(Departamento model)
        {

            using var connection = new SqlConnection(ConnectionString);

            var id = await connection.QuerySingleAsync<int>(
                @"INSERT INTO Departamentos
                (Nombre)
                VALUES
                (@Nombre);

                SELECT SCOPE_IDENTITY();", model);

            model.Id = id;

        }

        public async Task Edit(Departamento model)
        {

            using var connection = new SqlConnection(ConnectionString);

            await connection.ExecuteAsync
                (@"
                UPDATE Departamentos
                SET Nombre = @Nombre 
                WHERE Id = @Id;
                ", model);

        }

        public async Task Eliminar(int id)
        {

            using var connection = new SqlConnection(ConnectionString);

            await connection.ExecuteAsync(
               "DELETE FROM Departamentos WHERE Id = @Id;", new { id });

        }

        public async Task<bool> IsExist(string nombre)
        {

            using var connection = new SqlConnection(ConnectionString);

            var exist = await connection.QueryFirstOrDefaultAsync<int>(
                @"SELECT 
	                1
                FROM Departamentos
                WHERE Nombre = @Nombre;
                ", new { nombre });



            return exist == 1;

        }

        public async Task<IEnumerable<Departamento>> Get()
        {

            using var connection = new SqlConnection(ConnectionString);

            return await connection.QueryAsync<Departamento>(
                @"
                    SELECT 
	                    Id,
	                    Nombre
                    FROM Departamentos
                    ");


        }

        public async Task<Departamento> GetById(int id)
        {

            using var connection = new SqlConnection(ConnectionString);

            return await connection.QueryFirstOrDefaultAsync<Departamento>(
                @"
                    SELECT 
	                    Id,
	                    Nombre
                    FROM Departamentos
                    WHERE Id = @Id", new { id });


        }

    }
}
