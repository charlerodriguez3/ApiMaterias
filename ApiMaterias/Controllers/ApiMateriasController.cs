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
         
    }
}
