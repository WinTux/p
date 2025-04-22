using examen.Models;

namespace examen.Services
{
    public interface IPersonaService
    {
        public IEnumerable<Persona> GetAll();
        public Persona? GetById(int id);
    }
}
