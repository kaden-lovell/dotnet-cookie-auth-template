using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Server.Services;
namespace source.Controllers {
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Policy = "StudentRole")]
    [Authorize(Policy = "TeacherRole")]
    public class SpeechAceController : ControllerBase {
        private readonly ILogger<SpeechAceController> _logger;
        private readonly SpeechAceService _speechAceService;
        public SpeechAceController(ILogger<SpeechAceController> logger, SpeechAceService speechAceService) {
            _speechAceService = speechAceService;
            _logger = logger;
        }

        [HttpGet("ScorePronunciation")]
        public async Task<dynamic> ScorePronunciation([FromForm] IFormFile file, [FromBody] dynamic model) {
            using (var httpClient = new HttpClient())
            using (var request = new HttpRequestMessage(new HttpMethod("POST"), "https://api.speechace.co/api/scoring/text/v0.5/json?key={{speechacekey}}&dialect=en-us&user_id=XYZ-ABC-99001")) {
                var multipartContent = new MultipartFormDataContent();
                multipartContent.Add(new StringContent("\"apple\""), "text");
                // multipartContent.Add(new ByteArrayContent(File.ReadAllBytes("/Users/shimiz/speechace_dev/audio/misc/apple.wav")), "user_audio_file", Path.GetFileName("/Users/shimiz/speechace_dev/audio/misc/apple.wav"));
                multipartContent.Add(new StringContent("\""), "question_info");
                request.Content = multipartContent;

                var response = await httpClient.SendAsync(request);
                var result = await response.Content.ReadAsStringAsync();
                return new {
                    result
                };
            }
        }
    }
}