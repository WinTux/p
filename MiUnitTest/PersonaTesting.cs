using examen.Controllers;
using examen.Models;
using examen.Services;
using Microsoft.AspNetCore.Mvc;

namespace MiUnitTest
{
    public class PersonaTesting
    {
        private readonly PersonaController _personaController;
        private readonly IPersonaService _personaService;
        public PersonaTesting()
        {
            _personaService = new PersonaService();
            _personaController = new PersonaController(_personaService);
        }
        [Fact]
        public void Get_ok()
        {
            var resultado = _personaController.Get();
            Assert.IsType<OkObjectResult>(resultado);
        }
        [Fact]
        public void Get_cant()
        {
            var resultado = (OkObjectResult)_personaController.Get();
            var pers = Assert.IsType<List<Persona>>(resultado.Value);
            Assert.True(pers.Count > 0);
        }
        [Fact]
        public void GetById_ok()
        {
            int id = 1;
            var resultado = _personaController.GetById(id);
            Assert.IsType<OkObjectResult>(resultado);
        }
        [Fact]
        public void GetById_existe()
        {
            //Preparacion
            int id = 1;
            //Acto
            var resultado = (OkObjectResult)_personaController.GetById(id);
            //Afirmacion
            var per = Assert.IsType<Persona>(resultado?.Value);
            Assert.True(per != null);
            Assert.Equal(per?.id, id);
        }
        [Fact]
        public void GetById_No_existe()
        {
            //Preparacion
            int id = 123;
            //Acto
            var resultado = _personaController.GetById(id);
            //Afirmacion
            var per = Assert.IsType<NotFoundResult>(resultado);
        }
    }
}