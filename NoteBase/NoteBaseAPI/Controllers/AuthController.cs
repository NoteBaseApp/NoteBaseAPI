using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using NoteBaseLogicFactory;
using NoteBaseLogicInterface;
using NoteBaseLogicInterface.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace NoteBaseAPI.Controllers
{
    [Route("")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly string connString;
        private readonly IPersonProcessor personProcessor;

        public AuthController(IConfiguration config)
        {
            _config = config;
            string? DATA_SOURCE = Environment.GetEnvironmentVariable("DATA_SOURCE");
            string? INITIAL_CATALOG = Environment.GetEnvironmentVariable("INITIAL_CATALOG");
            string? DB_USER_ID = Environment.GetEnvironmentVariable("DB_USER_ID");
            string? DB_PASSWORD = Environment.GetEnvironmentVariable("DB_PASSWORD");
            connString = $"Data Source={DATA_SOURCE};Initial Catalog={INITIAL_CATALOG};User id={DB_USER_ID};Password={DB_PASSWORD};Connect Timeout=300;";
            personProcessor = ProcessorFactory.CreatePersonProcessor(connString);
        }

        //not sure what the param wil be, because i dont know that i will get from the Google auth
        [HttpPost("login")]
        [AllowAnonymous]
        public IActionResult Login([FromBody] String _userToken)
        {
            Person user = Authenticate(_userToken);

            if (user == null)
            {
                return NotFound("User not found");
            }

            string token = Generated(user);
            return Ok(token);
        }

        private string Generated(Person _user)
        {
            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("JWT_ACCESS_TOKEN_SECRET")));
            SigningCredentials credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            Claim[] claims = new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier, _user.Name),
                new Claim(ClaimTypes.Email, _user.Email)
            };

            JwtSecurityToken token = new JwtSecurityToken(
                Environment.GetEnvironmentVariable("JWT_ISSUER"), 
                Environment.GetEnvironmentVariable("JWT_AUDIENCE"), 
                claims, 
                expires: DateTime.Now.AddMinutes(60), 
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private Person Authenticate(string _userToken)
        {
            Person currentUser = personProcessor.GetByEmail(_userToken);

            if (currentUser.ID == new Guid("00000000-0000-0000-0000-000000000000"))
            {
                return null;
            }

            return currentUser;
        }
    }
}
