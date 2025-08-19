using Microsoft.AspNetCore.Mvc;

namespace GreeneKing.Talks.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SpeakerRegistrationController : ControllerBase
    {
        private readonly ILogger<SpeakerRegistrationController> _logger;

        public SpeakerRegistrationController(ILogger<SpeakerRegistrationController> logger)
        {
            _logger = logger;
        }
    }
}
