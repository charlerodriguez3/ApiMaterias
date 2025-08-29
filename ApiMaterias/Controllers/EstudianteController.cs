using ApiMaterias.Aplicacion.Interfaces;
using ApiMaterias.Dominio.Entidades;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiMaterias.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class EstudianteController : ControllerBase
    {
        private readonly IEstudiante _estudiante;
        public EstudianteController(IEstudiante estudiante)
        {
            _estudiante = estudiante;
        }
        
        [HttpPost("CrearEstudiante")]
        public async Task<IActionResult> CrearEstudiante(Estudiante estudiante)
        {
            try
            {
                return Ok(await _estudiante.CrearEstudiante(estudiante));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("GetEstudianteByCorreo/{correo}")]
        public async Task<IActionResult> GetEstudianteByCorreo(string correo)
        {
            try
            {
                return Ok(await _estudiante.GetEstudianteByCorreo(correo));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetMisMaterias/{id}")]
        public async Task<IActionResult> GetMisMaterias(int id)
        {
            try
            {
                return Ok(await _estudiante.GetMisMaterias(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
