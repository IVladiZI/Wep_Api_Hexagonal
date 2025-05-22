using Domain.Ports.Incoming;

namespace Application
{
    public class BusinessErrorDictionary : IBusinessErrorDictionary
    {
        private readonly Dictionary<string, string> _errors = new()
        {
            { "DEVICE_NO_ACCES", "Celular sin acceso a esta aplicación." },
            { "DEVICE_LOCKED", "Dispositivo bloqueado." },
            { "GENERIC_ERROR", "Ha ocurrido un error." },
            { "DEVICE_VALIDATION_FAILED", "La validación del dispositivo falló." }
            // Agrega más códigos y mensajes según tus necesidades
        };
        public string GetErrorMessage(string errorCode)
        {
            return _errors.TryGetValue(errorCode, out var message)
                ? message
                : "Error desconocido.";
        }
    }
}
