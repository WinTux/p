using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;

namespace ejemplo.Controllers
{
    [Authorize]
    [Route("validar")]
    [ApiController]
    public class IdentificadorController : ControllerBase
    {
        private const string clave = "UnaPruebaParaLaPrueba";
        private static readonly TimeSpan vida = TimeSpan.FromMinutes(30);
        [HttpPost("token")]
        public IActionResult Generar([FromBody] SolicitudDeToken token)
        {
            var x = new JwtSecurityTokenHandler();
            var llave = Encoding.UTF8.GetBytes(clave);
            var claims = new List<Claim>
            {
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new(JwtRegisteredClaimNames.Sub, token.email),
                new(JwtRegisteredClaimNames.Email, token.email),
                new("userid", token.userId.ToString())
            };
            foreach (var parClaim in token.claimPersonalizado) {
                var j = (JsonElement)parClaim.Ad;
            }
            return null;
        }
    }
}
