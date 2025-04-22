using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using prueba.Models;

namespace prueba.Controllers
{
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly ILogger<HomeController> _logger;
        string llave = "estaEsUnaClave123456ABCxyzEsUnaClaveLargaParaLaPrueba007";
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        [HttpGet("/")]
        public IActionResult Index()
        {
            return Ok();
        }
        [Authorize]
        [HttpGet("/privado")]
        public IActionResult Elemento()
        {
            return Ok();
        }
        [Authorize(Policy = "RequireScopeMiapiCosas")]
        [HttpGet("/privado2")]
        public IActionResult Elemento2()
        {
            return Ok("Para Pepe");
        }
        [HttpGet("/autenticar/{user}/{pass}")]
        public string Autenticar(string user, string pass)
        {
            if (user.Equals("Pepe") && pass.Equals("123456")) { 
                var tokenHandler = new JwtSecurityTokenHandler();
                var llaveByte = Encoding.UTF8.GetBytes(llave);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new System.Security.Claims.ClaimsIdentity(new Claim[]
                    {
                        new System.Security.Claims.Claim(ClaimTypes.Name, user),
                        new System.Security.Claims.Claim("pass", pass)
                    }),
                    Expires = DateTime.UtcNow.AddHours(3),
                    SigningCredentials = new Microsoft.IdentityModel.Tokens.SigningCredentials(
                        new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(llaveByte),
                        SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                return tokenHandler.WriteToken(token);
            }
            else
            {
                return "";
            }
        }
        [HttpGet("/autenticar2/{user}/{pass}")]
        public string Autenticar2(string user, string pass)
        {
            if (user.Equals("Ana") && pass.Equals("123456"))
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var llaveByte = Encoding.UTF8.GetBytes(llave);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new System.Security.Claims.ClaimsIdentity(new Claim[]
                    {
                        new System.Security.Claims.Claim(ClaimTypes.Name, user),
                        new System.Security.Claims.Claim("Scope", "miapi:cosa")
                    }),
                    Expires = DateTime.UtcNow.AddHours(3),
                    SigningCredentials = new Microsoft.IdentityModel.Tokens.SigningCredentials(
                        new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(llaveByte),
                        SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                return tokenHandler.WriteToken(token);
            }
            else
            {
                return "";
            }
        }
    }
}
