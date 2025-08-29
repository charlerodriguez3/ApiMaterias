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
    public class ApiMateriasController : ControllerBase
    {
        private readonly IMateria _materia;
        public ApiMateriasController(IMateria materia)
        {
            _materia = materia;
        }

        [HttpDelete("EliminarMateria/{idEstudiante}/{idMateria}")]
        public async Task<IActionResult> EliminarMateria(int idEstudiante, int idMateria)
        {
            try
            {
                return Ok(await _materia.EliminarMateria(idEstudiante,idMateria));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("CrearMateria")]
        public async Task<IActionResult> CrearMateria([FromBody]Materia materia)
        {
            try
            {
                return Ok(await _materia.CrearMateria(materia));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("GetCompaneros/{idEstudiante}")]

        public async Task<IActionResult> GetCompaneros(int idEstudiante)
        {
            try
            {
                return Ok(await _materia.GetCompaneros(idEstudiante));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
