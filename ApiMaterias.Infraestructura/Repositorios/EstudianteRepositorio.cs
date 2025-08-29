using ApiMaterias.Dominio.Entidades;
using ApiMaterias.Infraestructura.Conexion;
using ApiMaterias.Infraestructura.Interfaces;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Data.SqlClient;

namespace ApiMaterias.Infraestructura.Repositorios
{
    public class EstudianteRepositorio : IEstudianteRepo
    {
        private readonly EscuelaContext _db;
        
        public EstudianteRepositorio(EscuelaContext contextoDB)
        {
            _db = contextoDB;
        }

        public async Task<Estudiante?> CrearEstudiante(Estudiante estudiante)
        {
            using (SqlConnection conn = new SqlConnection(_db.CrearConexion()))
            {
                try
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("sp_insertarEstudiante", conn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Nombre", estudiante.Nombre);
                        cmd.Parameters.AddWithValue("@Apellido", estudiante.Apellido);
                        cmd.Parameters.AddWithValue("@Correo", estudiante.Correo);
                        cmd.Parameters.AddWithValue("@Clave", estudiante.Clave);

                        int rowsAffected = await cmd.ExecuteNonQueryAsync();
                        if (rowsAffected > 0)
                        {
                            return estudiante;
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
                catch (SqlException ex)
                {
                    throw new Exception("Error al crear estudiante", ex);
                }
            }
        }

        public async Task<Estudiante?> GetEstudianteByCorreo(string correo)
        {
            using (SqlConnection conn = new SqlConnection(_db.CrearConexion()))
            {
                try
                {
                    await conn.OpenAsync();

                    using (SqlCommand cmd = new SqlCommand("sp_traerEstudiante", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Correo", correo);

                        using (SqlDataReader reader = await cmd.ExecuteReaderAsync().ConfigureAwait(false))
                        {
                            if (await reader.ReadAsync().ConfigureAwait(false))
                            {
                                return new Estudiante
                                {
                                    Id = reader.GetInt32(reader.GetOrdinal("E_id")),
                                    Nombre = reader.GetString(reader.GetOrdinal("E_nombre")),
                                    Apellido = reader.GetString(reader.GetOrdinal("E_apellido")),
                                    Correo = reader.GetString(reader.GetOrdinal("E_correo")),
                                    Clave = reader.GetString(reader.GetOrdinal("E_clave")),
                                    Creditos = reader.GetInt32(reader.GetOrdinal("E_creditos"))
                                };
                            }
                            return null;
                        }
                    }
                }
                catch (SqlException ex)
                {
                    throw new Exception("Error al obtener estudiante", ex);
                }
            }
        }

        public async Task<List<MateriasXEstudiante>> GetMisMaterias(int id)
        {
            List<MateriasXEstudiante> materias = new List<MateriasXEstudiante>();
            using (SqlConnection conn = new SqlConnection(_db.CrearConexion()))
            {
                try
                {
                    await conn.OpenAsync();

                    using (SqlCommand cmd = new SqlCommand("sp_registrosMisMaterias", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Id", id);

                        using (SqlDataReader reader = await cmd.ExecuteReaderAsync().ConfigureAwait(false))
                        {
                            while(await reader.ReadAsync().ConfigureAwait(false))
                            {
                                materias.Add( new MateriasXEstudiante{
                                    ClaseId = reader.GetInt32(reader.GetOrdinal("C_id")),
                                    ProfeId = reader.GetInt32(reader.GetOrdinal("P_id")),
                                    NombreProfe = reader.GetString(reader.GetOrdinal("P_nombre")),
                                    ApellidoProfe = reader.GetString(reader.GetOrdinal("P_apellido")),
                                    MateriaId = reader.GetInt32(reader.GetOrdinal("M_id")),
                                    NombreMateria= reader.GetString(reader.GetOrdinal("M_nombre")),
                                    CreditosMateria = reader.GetInt32(reader.GetOrdinal("M_creditos"))
                                });
                            }
                            return materias;
                        }
                    }
                }
                catch (SqlException ex)
                {
                    throw new Exception("Error al obtener mis materias", ex);
                }
            }
        }

        public async Task<bool> ValidarCredenciales(string correo, string clave)
        {

            using(SqlConnection conn = new SqlConnection(_db.CrearConexion()))
            {
                try
                {
                    conn.Open();
                    using(SqlCommand cmd = new SqlCommand("sp_validarIngreso", conn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Correo", correo);
                        cmd.Parameters.AddWithValue("@Clave", clave);
                        var result = await cmd.ExecuteScalarAsync();
                        if (result == DBNull.Value || result == null)
                        {
                            return false;
                        }
                        else
                        {
                            return true;
                        }    
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error al validar credenciales", ex);
                }
            }
        }
    }
}
