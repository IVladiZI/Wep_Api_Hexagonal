using Domain.Exceptions;
using Domain.Ports.Outgoing;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Infrastructure.Repositories
{
    public class DeviceRepository : IDevicePersistence
    {
        private readonly string _connectionString;

        public DeviceRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<long> InsertDeviceAsync(string deviceId, string deviceType)
        {
            try
            {
                using var connection = new SqlConnection(_connectionString);
                using var command = new SqlCommand("bil.DISPOSITIVO_InsertMod", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                command.Parameters.AddWithValue("@DSPT_ID_VC", deviceId);
                command.Parameters.AddWithValue("@DSPT_TIPO_VC", deviceType);

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
                throw new GenericError("Error al insertar el dispositivo en la base de datos.", ex);
            }
        }
    }
}
