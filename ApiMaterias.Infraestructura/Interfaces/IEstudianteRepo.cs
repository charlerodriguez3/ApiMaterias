using ApiMaterias.Dominio.Entidades;

namespace ApiMaterias.Infraestructura.Interfaces
{
    public interface IEstudianteRepo
    {
        Task<Estudiante?> CrearEstudiante(Estudiante estudiante);
        Task<bool> ValidarCredenciales(string correo, string clave);
        Task<Estudiante?> GetEstudianteByCorreo(string correo);

        Task<List<MateriasXEstudiante>> GetMisMaterias(int id);
    }
}
