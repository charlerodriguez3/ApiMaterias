using ApiMaterias.Aplicacion.Interfaces;
using ApiMaterias.Dominio.Entidades;
using ApiMaterias.Infraestructura.Interfaces;

namespace ApiMaterias.Aplicacion.Servicios
{
    public class ServicioEstudiante : IEstudiante
    {
        private readonly IEstudianteRepo _repo;
        
        public ServicioEstudiante(IEstudianteRepo repo)
        {
            _repo = repo;
        }

        public async Task<Estudiante> CrearEstudiante(Estudiante estudiante)
        {
            return await _repo.CrearEstudiante(estudiante);    
        }

        public async Task<Estudiante> GetEstudianteByCorreo(string correo)
        {
            return await _repo.GetEstudianteByCorreo(correo);
        }

        public async Task<bool> ValidarCredenciales(string correo, string clave)
        {
            return await _repo.ValidarCredenciales(correo, clave);
        }
    }
}
