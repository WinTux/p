using examen.Models;
using examen.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace examen.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PersonaController : ControllerBase
    {
        private readonly IPersonaService _personaService;
        public PersonaController(IPersonaService personaService)
        {
            _personaService = personaService;
        }
        [HttpGet]
        public ActionResult Get()
        {
            var personas = _personaService.GetAll();
            return Ok(personas);
        }
        [HttpGet("{id}")]
        public ActionResult GetById(int id)
        {
            var persona = _personaService.GetById(id);
            if (persona == null)
            {
                return NotFound();
            }
            return Ok(persona);
        }
    }
}
