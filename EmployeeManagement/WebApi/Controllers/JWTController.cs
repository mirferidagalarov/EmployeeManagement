using Business.Security;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JWTController : ControllerBase
    {
        private readonly IConfiguration _config;

        public JWTController(IConfiguration config)
        {
            _config = config;
        }

        //[HttpGet]
        //public IActionResult Get()
        //{
        //    Token token = TokenHandler.CreateToken(_config);
        //    return Ok(token);
        //}

    }
}
