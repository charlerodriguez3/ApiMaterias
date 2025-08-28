namespace ApiMaterias.Dominio.Entidades
{
    public class Estudiante
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public string? Apellido { get; set; }
        public string? Correo { get; set; }
        public string? Clave { get; set; }
        public int Creditos { get; set; }
    }
}
