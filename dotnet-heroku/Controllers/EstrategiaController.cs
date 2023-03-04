using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace dotnet_heroku.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class EstrategiaController : ControllerBase
    {
        //private readonly IEstrategiaService _service;
        private readonly NinjaTraderContext _context;

        public EstrategiaController()
        {
            //_service = service;
        }        
        
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(EstrategiaResponse))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ProblemDetails))]
        public async Task<IActionResult> Get() 
            => Ok(await _context.Estrategias.ToListAsync()); 
        
    }
}