using examen.Models;

namespace examen.Services
{
    public class PersonaService : IPersonaService
    {
        private List<Persona> personaList = new List<Persona>
            {
                new Persona { id = 1, nombre = "Juan", apellido = "Pérez" },
                new Persona { id = 2, nombre = "Ana", apellido = "García" }
            };
        public IEnumerable<Persona> GetAll()
        {
            return personaList;
        }
        public Persona? GetById(int id)
        {
            return personaList.FirstOrDefault(p=>p.id == id);
        }
    }
}
