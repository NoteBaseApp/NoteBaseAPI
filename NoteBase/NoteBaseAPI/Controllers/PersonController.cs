using Microsoft.AspNetCore.Mvc;
using NoteBaseAPI.Models;
using NoteBaseInterface;
using NoteBaseLogic;
using NoteBaseLogicFactory;
using NoteBaseLogicInterface;
using NoteBaseLogicInterface.Models;
using System.Security.Cryptography;

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
            connString = Environment.GetEnvironmentVariable("DATABASE_URL");
            personProcessor = ProcessorFactory.CreatePersonProcessor(connString);
        }

        // GET api/<PersonController>/example@gmail.com
        [HttpGet("{_email}")]
        public APIResponse Get(string _email)
        {
            APIResponse response = new(APIResponseStatus.Success);

            Person person = personProcessor.GetByEmail(_email);

            if (person.ID == Guid.Parse("00000000-0000-0000-0000-000000000000"))
            {
                response.Status = APIResponseStatus.Failure;
                response.Message = "Person does not exist.";
                return response;
            }

            response.ResponseBody = person;
            return response;
        }

        /* // POST api/<PersonController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
            throw new NotImplementedException();
        }

        // PUT api/<PersonController>/5
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
