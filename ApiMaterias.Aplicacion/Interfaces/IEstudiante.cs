using ApiMaterias.Dominio.Entidades;

namespace ApiMaterias.Aplicacion.Interfaces
{
    public interface IEstudiante
    {
        Task<Estudiante> CrearEstudiante(Estudiante estudiante);
        Task<bool> ValidarCredenciales(string correo, string clave);

        Task<Estudiante> GetEstudianteByCorreo(string correo);


        Task<List<MateriasXEstudiante>> GetMisMaterias(int id);
    }
}
