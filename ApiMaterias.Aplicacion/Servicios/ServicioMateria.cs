using ApiMaterias.Aplicacion.Interfaces;
using ApiMaterias.Dominio.Entidades;
using ApiMaterias.Infraestructura.Interfaces;

namespace ApiMaterias.Aplicacion.Servicios
{
    public class ServicioMateria : IMateria
    {
        private readonly IMateriaRepo _repo;

        public ServicioMateria(IMateriaRepo repo)
        {
            _repo = repo;
        }

        public async Task<bool> CrearMateria(Materia materia)
        {
            return await _repo.CrearMateria(materia);
        }

        public async Task<bool> EliminarMateria(int idEstudiante, int idMateria)
        {
            return await _repo.EliminarMateria(idEstudiante, idMateria);
        }

        public async Task<List<CompanerosClase>> GetCompaneros(int idEstudiante)
        {
            return await _repo.GetCompaneros(idEstudiante);     
        }
    }
}
