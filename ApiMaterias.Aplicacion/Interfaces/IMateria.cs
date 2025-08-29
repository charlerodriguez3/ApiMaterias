
using ApiMaterias.Dominio.Entidades;

namespace ApiMaterias.Aplicacion.Interfaces
{
    public interface IMateria
    {
        public Task<bool> EliminarMateria(int idEstudiante, int idMateria);

        public Task<bool> CrearMateria(Materia materia);

        public Task<List<CompanerosClase>> GetCompaneros(int idEstudiante);
    }
}
