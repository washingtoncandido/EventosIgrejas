using FICR.Cooperation.Humanism.Services.Facebook;
using Microsoft.AspNetCore.Mvc;

namespace FICR.Cooperation.Humanism.APIs.Controllers
{
    public class SendMessageWhappController : ControllerBase
    {
        private readonly FacebookApiService _facebookApiService;

        public SendMessageWhappController(FacebookApiService facebookApiService)
        {
            _facebookApiService = facebookApiService;
        }

        [HttpPost("send-message")]
        public async Task<IActionResult> SendMessage(string recipientPhoneNumber, string templateName)
        {
            await _facebookApiService.SendMenssageAsync(recipientPhoneNumber, templateName);

            return Ok("Message sent successfully.");
        }
    }
}
