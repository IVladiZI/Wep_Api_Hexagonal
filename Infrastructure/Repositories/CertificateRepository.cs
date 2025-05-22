using Domain.Entities;
using Domain.Exceptions;
using Domain.Ports.Outgoing;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Infrastructure.Repositories
{
    public class CertificateRepository : ICertificatePersistence
    {
        private readonly string _connectionString;

        public CertificateRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<long> InsertCertificateAsync(long dispositivoId,string stringKey, string stringIV)
        {
            try
            {
                using var connection = new SqlConnection(_connectionString);
                using var command = new SqlCommand("bill.DISPOSITIVO_CERTIFICADO_Insert", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                command.Parameters.AddWithValue("@DICE_DSPT_ID_IT", dispositivoId);
                command.Parameters.AddWithValue("@DICE_KEY_VC", stringKey);
                command.Parameters.AddWithValue("@DICE_IV_VC", stringIV);

                await connection.OpenAsync();

                using var reader = await command.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    var identityValue = reader["@@IDENTITY"];
                    if (identityValue != DBNull.Value)
                    {
                        return Convert.ToInt64(identityValue);
                    }
                }

                throw new GenericError("Dispositivo Certificado no identificado");
            }
            catch (SqlException ex)
            {
                throw new GenericError("Error al insertar el certificado en la base de datos.", ex);
            }
        }
    }
}
