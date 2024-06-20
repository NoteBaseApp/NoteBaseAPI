using Microsoft.AspNetCore.Authorization;
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
    [Route("tag")]
    [ApiController]
    public class TagController : ControllerBase
    {
        private readonly string connString;
        private readonly ITagProcessor tagProcessor;

        public TagController()
        {
            string? DATA_SOURCE = Environment.GetEnvironmentVariable("DATA_SOURCE");
            string? INITIAL_CATALOG = Environment.GetEnvironmentVariable("INITIAL_CATALOG");
            string? DB_USER_ID = Environment.GetEnvironmentVariable("DB_USER_ID");
            string? DB_PASSWORD = Environment.GetEnvironmentVariable("DB_PASSWORD");
            connString = $"Data Source={DATA_SOURCE};Initial Catalog={INITIAL_CATALOG};User id={DB_USER_ID};Password={DB_PASSWORD};Connect Timeout=300;";
            tagProcessor = ProcessorFactory.CreateTagProcessor(connString);
        }

        // GET: api/<TagController>/5
        [HttpGet("GetByPerson/{_personId}")]
        [Authorize]
        public APIResponse GetByPerson(Guid _personId)
        {
            APIResponse response = new(APIResponseStatus.Success);

            List<Tag> tag = tagProcessor.GetByPerson(_personId);

            if (tag == null || tag.Count == 0)
            {
                response.Status = APIResponseStatus.Failure;
                response.Message = "No tags where found.";
                return response;
            }

            response.ResponseBody = tag;
            return response;
        }

        // GET: api/<TagController>/
        [HttpGet("GetByTitle/{_Title}")]
        [Authorize]
        public APIResponse GetByTitle(string _Title)
        {
            APIResponse response = new(APIResponseStatus.Success);

            Tag tag = tagProcessor.GetByTitle(_Title);

            if (tag.ID == Guid.Parse("00000000-0000-0000-0000-000000000000"))
            {
                response.Status = APIResponseStatus.Failure;
                response.Message = "Tag does not exist.";
                return response;
            }

            response.ResponseBody = tag;
            return response;
        }

        // GET api/<TagController>/5
        [HttpGet("{_id}")]
        public APIResponse Get(Guid _id)
        {
            APIResponse response = new(APIResponseStatus.Success);

            Tag tag = tagProcessor.GetById(_id);

            if (tag.ID == Guid.Parse("00000000-0000-0000-0000-000000000000"))
            {
                response.Status = APIResponseStatus.Failure;
                response.Message = "Tag does not exist.";
                return response;
            }

            response.ResponseBody = tag;
            return response;
        }
    }
}
