using ApiMaterias.Dominio.Entidades;
using ApiMaterias.Infraestructura.Conexion;
using ApiMaterias.Infraestructura.Interfaces;
using Microsoft.Data.SqlClient;
using System.Data;

namespace ApiMaterias.Infraestructura.Repositorios
{
    public class MateriaRepositorio : IMateriaRepo
    {

        private readonly EscuelaContext _db;

        public MateriaRepositorio(EscuelaContext contextoDB)
        {
            _db = contextoDB;
        }
        public async Task<bool> EliminarMateria(int idEstudiante, int idMateria)
        {
            using (SqlConnection conn = new SqlConnection(_db.CrearConexion()))
            {
                try
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("sp_eliminarMateria", conn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Idmateria", idMateria);
                        cmd.Parameters.AddWithValue("@Idestudiante", idEstudiante);

                        int rowsAffected = await cmd.ExecuteNonQueryAsync();
                        if (rowsAffected > 0)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
                catch (SqlException ex)
                {
                    throw new Exception("Error al eliminar la materia", ex);
                }
            }
        }

        public async Task<bool> CrearMateria(Materia materia)
        {
            using (SqlConnection conn = new SqlConnection(_db.CrearConexion()))
            {
                try
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("sp_insertarClase", conn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Idprofesor", materia.IdProfesor);
                        cmd.Parameters.AddWithValue("@Idmateria", materia.IdMateria);
                        cmd.Parameters.AddWithValue("@Idestudiante", materia.IdEstudiante);

                        SqlParameter resultadoParam = new SqlParameter("@ReturnValue", System.Data.SqlDbType.Int)
                        {
                            Direction = System.Data.ParameterDirection.Output
                        };
                        cmd.Parameters.Add(resultadoParam);
                        await cmd.ExecuteNonQueryAsync();
                        return Convert.ToBoolean((int)resultadoParam.Value);

                    }
                }
                catch (SqlException ex)
                {
                    throw new Exception("Error al crear la materia", ex);
                }
            }
        }

        public async Task<List<CompanerosClase>> GetCompaneros(int idEstudiante)
        {
            List<CompanerosClase> materias = new List<CompanerosClase>();
            using (SqlConnection conn = new SqlConnection(_db.CrearConexion()))
            {
                try
                {
                    await conn.OpenAsync();

                    using (SqlCommand cmd = new SqlCommand("sp_companerosDeClase", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Id", idEstudiante);

                        using (SqlDataReader reader = await cmd.ExecuteReaderAsync().ConfigureAwait(false))
                        {
                            while (await reader.ReadAsync().ConfigureAwait(false))
                            {
                                materias.Add(new CompanerosClase
                                {
                                    NombreEstudiante = reader.GetString(reader.GetOrdinal("NombreEstudiante")),
                                    ApellidoEstudiante = reader.GetString(reader.GetOrdinal("ApellidoEstudiante")),
                                    CodigoMateria = reader.GetInt32(reader.GetOrdinal("CodigoMateria")),
                                    NombreMateria = reader.GetString(reader.GetOrdinal("NombreMateria")),
                                    CreditosMateria = reader.GetInt32(reader.GetOrdinal("CreditosMateria"))
                                });
                            }
                            return materias;
                        }
                    }
                }
                catch (SqlException ex)
                {
                    throw new Exception("Error al obtener companeros de clase", ex);
                }
            }
        }
    }
}
