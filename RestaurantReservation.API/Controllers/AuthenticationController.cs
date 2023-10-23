using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestaurantReservation.API.Authentication;

namespace RestaurantReservation.API.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly JwtTokenGenerator _tokenGenerator;

        public AuthenticationController(JwtTokenGenerator tokenGenerator)
        {
            _tokenGenerator = tokenGenerator;
        }

        [HttpPost("generate-token")]
        public IActionResult GenerateToken( AuthenticationRequestBody requestBody)
        {
            var token = _tokenGenerator.GenerateToken(requestBody);

            if (token != null)
            {
                return Ok(new { Token = token });
            }
            else
            {
                return Unauthorized("Invalid credentials");
            }
        }
    }
}
