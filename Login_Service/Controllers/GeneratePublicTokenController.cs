using Domain.Entities;
using Domain.Ports.Incoming;
using Domain.Ports.Outgoing;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Login_Service.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GeneratePublicTokenController : ControllerBase
    {
        private readonly IGeneratePublicToken _generatePublicToken;
        private readonly ISessionHistoryPersistence _sessionHistoryPersistence;

        public GeneratePublicTokenController(IGeneratePublicToken generatePublicToken, ISessionHistoryPersistence sessionHistoryPersistence)
        {
            _generatePublicToken = generatePublicToken;
            _sessionHistoryPersistence = sessionHistoryPersistence;
        }

        [HttpPost("GeneratePublicToken")]
        public async Task<IActionResult> GeneratePublicToken([FromBody] AuthenticationDevice authenticationDevice)
        {
            var startTimeRequest = DateTime.UtcNow;
            var response = await _generatePublicToken.GenerateTokenAsync(authenticationDevice);
            var EndTimeRequest = DateTime.UtcNow;

            var SessionHistory = new SessionHistory
            {
                TypeRequestId = 1,
                PublicToken = response.Data == null ? null : ((CertifiedPublic)response.Data).AuthToken,
                PrivateToken = null,
                Request = JsonSerializer.Serialize(authenticationDevice),
                Response = JsonSerializer.Serialize(response),
                StartTimeRequest = startTimeRequest,
                EndTimeRequest = startTimeRequest,
                StateResponse = response.State
            };

            _ = _sessionHistoryPersistence.InsertSessionHistory(SessionHistory);

            if (response.State == 200)
                return Ok(response);
            else
                return BadRequest(response);
        }
    }
}
