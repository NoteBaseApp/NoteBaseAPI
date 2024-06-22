using Microsoft.AspNetCore.Mvc;
using NoteBaseLogicFactory;
using NoteBaseLogicInterface;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NoteBaseAPI.Controllers
{
    [Route("person")]
    [ApiController]
    public class PersonController : ControllerBase
    {

        private readonly string connString;
        private readonly IPersonProcessor personProcessor;

        public PersonController()
        {
            string? DATA_SOURCE = Environment.GetEnvironmentVariable("DATA_SOURCE");
            string? INITIAL_CATALOG = Environment.GetEnvironmentVariable("INITIAL_CATALOG");
            string? DB_USER_ID = Environment.GetEnvironmentVariable("DB_USER_ID");
            string? DB_PASSWORD = Environment.GetEnvironmentVariable("DB_PASSWORD");
            connString = $"Data Source={DATA_SOURCE};Initial Catalog={INITIAL_CATALOG};User id={DB_USER_ID};Password={DB_PASSWORD};Connect Timeout=300;";
            personProcessor = ProcessorFactory.CreatePersonProcessor(connString);
        }

        /* [HttpGet("{_email}")]
        public IActionResult Get(string _email)
        {
            Person person = personProcessor.GetByEmail(_email);

            if (person.ID == Guid.Parse("00000000-0000-0000-0000-000000000000"))
            {
                return NotFound(new Error("DoesNotExist", "Person does not exist."));
            }

            return Ok(person);
        } */

        /* [HttpPost]
        public void Post([FromBody] string value)
        {
            throw new NotImplementedException();
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
            throw new NotImplementedException();
        }

        // DELETE api/<PersonController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            throw new NotImplementedException();
        }*/
    }
}
