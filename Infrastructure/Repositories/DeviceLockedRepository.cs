using Domain.Exceptions;
using Domain.Ports.Outgoing;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Infrastructure.Repositories
{
    public class DeviceLockedRepository : IDeviceLockedPersistence
    {
        private readonly string _connectionString;
        public DeviceLockedRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
        public async Task<long> InsertDeviceLocked(string deviceId, string deviceType, string encriptedDeviceId, string seedId)
        {
            try
            {
                using var connection = new SqlConnection(_connectionString);
                using var command = new SqlCommand("bill.DISPOSITIVO_BLOQUEADO_Insert", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                command.Parameters.AddWithValue("@DIBO_DISP_ID_VC", deviceId);
                command.Parameters.AddWithValue("@DIBO_DISP_TIPO_VC", deviceType);
                command.Parameters.AddWithValue("@DIDO_ENCRIPTADO_ID_VC", encriptedDeviceId);
                command.Parameters.AddWithValue("@DIBO_DISE_ID_VC", seedId);

                await connection.OpenAsync();

                using var reader = await command.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    var identityValue = reader["@@IDENTITY"];
                    if (identityValue != DBNull.Value)
                        return Convert.ToInt64(identityValue);
                }
                throw new GenericError("Dispositivo Bloqueado no identificado");
            }
            catch (Exception ex)
            {
                throw new GenericError("Exception", ex.Message);
            }
            
        }
    }
}
