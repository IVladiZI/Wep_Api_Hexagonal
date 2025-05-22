using Domain.Exceptions;
using Domain.Ports.Outgoing;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Infrastructure.Repositories
{
    public class PublicSessionRepository : IPublicSessionPersistence
    {
        private readonly string _connectionString;
        public PublicSessionRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
        public async Task<long> InsertPublicSessionAsync(long deviceId, Guid publicToken)
        {
            try
            {
                using var connection = new SqlConnection(_connectionString);
                using var command = new SqlCommand("bil.SESSION_InsertPublic", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                command.Parameters.AddWithValue("@SESS_DSPT_ID_IT", deviceId);
                command.Parameters.AddWithValue("@SESS_PUBLICA_UN", publicToken);

                await connection.OpenAsync();

                using var reader = await command.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    var identityValue = reader["@@IDENTITY"];
                    if (identityValue != DBNull.Value)
                        return Convert.ToInt64(identityValue);
                }

                throw new GenericError("Dispositivo no identificado");
            }
            catch (SqlException ex)
            {
                throw new GenericError("Exception", ex.Message);
            }
        }
    }
}
