using Microsoft.AspNetCore.Mvc;
using SafeScribe.Business;
using SafeScribe.Model;

namespace SafeScribe.API.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly UserService _userService;
        private readonly JwtService _jwtService;

        public AuthController(UserService userService, JwtService jwtService)
        {
            _userService = userService;
            _jwtService = jwtService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var user = await _userService.AuthenticateAsync(request.Username, request.Password);

            if (user == null)
                return Unauthorized("Usuário ou senha inválidos");

            var token = _jwtService.GenerateToken(user);

            return Ok(new { token });
        }
    }
}
