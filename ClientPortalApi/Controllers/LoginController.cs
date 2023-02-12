using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ClientPortalApi.Persistence;
using ClientPortalApi.Services;

namespace ClientPortalApi.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : Controller {
        private readonly DataContext _context;
        private readonly LoginService _loginService;
        public LoginController(DataContext context, LoginService loginService) {
            _context = context;
            _loginService = loginService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync([FromBody] dynamic model) {
            var result = await _loginService.LoginAsync(model);
            return Json(result);
        }

        [HttpPost("logout")]
        public async Task<IActionResult> LogoutAsync() {
            await _loginService.LogoutAsync();
            return NoContent();
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> RefreshCookieAsync([FromBody] dynamic model) {
            var result = await _loginService.LoginAsync(model);
            return Json(result);
        }
    }
}