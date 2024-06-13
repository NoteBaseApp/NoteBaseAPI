using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NoteBaseAPI.Models;
using NoteBaseLogic;
using NoteBaseLogicInterface.Models;

namespace NoteBaseAPI.Controllers
{
    [Route("")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        //not sure what the param wil be, because i dont know that i will get from the Google auth
        [HttpPost("/login")]
        public IActionResult GetByPerson([FromBody] String _userToken)
        {

            //auth user?

            return Ok(new { accessToken = Jwt.JsonWebToken.Encode(new { token = _userToken}, Environment.GetEnvironmentVariable("JWT_ACCESS_TOKEN_SECRET")) });
        }
    }
}
