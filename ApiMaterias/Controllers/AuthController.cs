using ApiMaterias.Aplicacion.Interfaces;
using ApiMaterias.Dominio.Entidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace ApiMaterias.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IEstudiante _estudianteService;

        public AuthController(IConfiguration configuration, IEstudiante estudianteService)
        {
            _configuration = configuration;
            _estudianteService = estudianteService;
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] Estudiante dto)
        {
            if (!await _estudianteService.ValidarCredenciales(dto.Correo!, dto.Clave!))
                return Unauthorized();

            var secretKey = _configuration["Jwt:Key"]!;
            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));

            var jwt = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256)
            );

            var token = new JwtSecurityTokenHandler().WriteToken(jwt);
            return Ok(new { token });
        }
    }
}
