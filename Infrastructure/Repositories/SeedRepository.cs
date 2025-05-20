using Domain.Exceptions;
using Domain.Ports.Outgoing;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Infrastructure.Repositories
{
    public class SeedRepository : ISeedPersistence
    {
        private readonly string _connectionString;

        public SeedRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<string> GetSemillaBySendIdAsync(string semillaId)
        {
            try
            {
                using var connection = new SqlConnection(_connectionString);
                using var command = new SqlCommand("DISPOSITIVO_SEMILLA_GetById", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                command.Parameters.AddWithValue("@DISE_ID_VC", semillaId);
                await connection.OpenAsync();

                using var reader = await command.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    var seedValue = reader["DISE_SEED_VC"];
                    if (seedValue != DBNull.Value)
                        return seedValue.ToString()!;
                }

                throw new GenericError("Dispositivo Semilla no identificado");
            }
            catch (SqlException ex)
            {
                throw new GenericError("Error al consultar la semilla en la base de datos.", ex);
            }
        }
    }
}
