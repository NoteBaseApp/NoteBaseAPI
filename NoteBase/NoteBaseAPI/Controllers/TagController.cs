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
            connString = Environment.GetEnvironmentVariable("DATABASE_URL");
            tagProcessor = ProcessorFactory.CreateTagProcessor(connString);
        }

        // GET: api/<TagController>/5
        [HttpGet("GetByPerson/{_personId}")]
        public APIResponse GetByPerson(int _personId)
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
        public APIResponse GetByTitle(string _Title)
        {
            APIResponse response = new(APIResponseStatus.Success);

            Tag tag = tagProcessor.GetByTitle(_Title);

            if (tag.ID == 0)
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
        public APIResponse Get(int _id)
        {
            APIResponse response = new(APIResponseStatus.Success);

            Tag tag = tagProcessor.GetById(_id);

            if (tag.ID == 0)
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
