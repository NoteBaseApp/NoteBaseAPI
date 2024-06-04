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
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {

        private readonly string connString;
        private readonly IPersonProcessor personProcessor;

        public PersonController()
        {
            connString = "Data Source=172.17.0.3,1433;Initial Catalog=NoteBase;User id=NoteBaseAPI;Password=K00kW3kk3r!;Connect Timeout=300;";
            personProcessor = ProcessorFactory.CreatePersonProcessor(connString);
        }

        // GET api/<PersonController>/example@gmail.com
        [HttpGet("{_email}")]
        public APIResponse Get(string _email)
        {
            APIResponse response = new(APIResponseStatus.Success);

            Person person = personProcessor.GetByEmail(_email);

            if (person.ID == 0)
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
