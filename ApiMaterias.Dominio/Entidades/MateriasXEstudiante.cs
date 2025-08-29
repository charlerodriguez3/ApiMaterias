
namespace ApiMaterias.Dominio.Entidades
{
    public class MateriasXEstudiante
    {
        public int ClaseId { get; set; }
        public int ProfeId { get; set; }
        public string? NombreProfe { get; set; }
        public string? ApellidoProfe { get; set; }
        public int MateriaId { get; set; }
        public string? NombreMateria { get; set; }
        public int CreditosMateria { get; set; }
    }
}
