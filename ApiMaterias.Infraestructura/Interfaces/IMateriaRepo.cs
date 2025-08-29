using ApiMaterias.Dominio.Entidades;

namespace ApiMaterias.Infraestructura.Interfaces
{
    public interface IMateriaRepo
    {
        Task<bool> EliminarMateria(int idEstudiante, int idMateria);

        Task<bool> CrearMateria(Materia materia);

        Task<List<CompanerosClase>> GetCompaneros(int idEstudiante);
    }
}
