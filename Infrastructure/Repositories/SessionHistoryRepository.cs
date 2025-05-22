using Domain.Entities;
using Domain.Exceptions;
using Domain.Ports.Outgoing;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Infrastructure.Repositories
{
    public class SessionHistoryRepository : ISessionHistoryPersistence
    {
        private readonly string _connectionString;

        public SessionHistoryRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
        public async void InsertSessionHistory(SessionHistory sessionHistory)
        {
            try
            {
                using var connection = new SqlConnection(_connectionString);
                using var command = new SqlCommand("bill.HISTORIAL_SESSION_Insert", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                command.Parameters.AddWithValue("@HISE_SESS_PUBLICA_UN", sessionHistory.PublicToken);
                command.Parameters.AddWithValue("@HISE_SESS_PRIVADA_UN", sessionHistory.PrivateToken);
                command.Parameters.AddWithValue("@HISE_HITI_ID_IT", sessionHistory.TypeRequestId);
                command.Parameters.AddWithValue("@HISE_SOLICITUD_VC", sessionHistory.Request);
                command.Parameters.AddWithValue("@HISE_RESPUESTA_VC", sessionHistory.Response);
                command.Parameters.AddWithValue("@HISE_FECHA_SOLICITUD_DT", sessionHistory.StartTimeRequest);
                command.Parameters.AddWithValue("@HISE_FECHA_RESPUESTA_DT", sessionHistory.EndTimeRequest);
                command.Parameters.AddWithValue("@HISE_RESPUESTA_IT", sessionHistory.StateResponse);

                await connection.OpenAsync();

            }
            catch (SqlException ex)
            {
                throw new GenericError("Error al insertar el certificado en la base de datos.", ex);
            }
        }
    }
    {

    }
}
