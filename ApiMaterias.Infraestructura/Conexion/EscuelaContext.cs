using Microsoft.Data.SqlClient;
using System.Data;

namespace ApiMaterias.Infraestructura.Conexion
{
    public class EscuelaContext
    {
        private readonly string _connectionString;

        public EscuelaContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        public string CrearConexion()
        {
            return _connectionString;
        }
    }
}
