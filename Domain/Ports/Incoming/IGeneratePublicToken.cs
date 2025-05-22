using Domain.Entities;

namespace Domain.Ports.Incoming
{
    public interface IGeneratePublicToken
    {
        Task<ApiResponse> GenerateTokenAsync(AuthenticationDevice authenticationDevice);
    }
}
