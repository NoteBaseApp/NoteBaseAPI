using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NoteBaseAPI.Models;
using NoteBaseLogicFactory;
using NoteBaseLogicInterface;
using NoteBaseLogicInterface.Models;
using System.Security.Claims;
using UI.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NoteBaseAPI.Controllers
{
    [Route("note")]
    [ApiController]
    public class NoteController : ControllerBase
    {
        private readonly string connString;
        private readonly IPersonProcessor personProcessor; // for when the DoesPersonExist method gets added
        private readonly INoteProcessor noteProcessor;

        public NoteController() 
        {
            string? DATA_SOURCE = Environment.GetEnvironmentVariable("DATA_SOURCE");
            string? INITIAL_CATALOG = Environment.GetEnvironmentVariable("INITIAL_CATALOG");
            string? DB_USER_ID = Environment.GetEnvironmentVariable("DB_USER_ID");
            string? DB_PASSWORD = Environment.GetEnvironmentVariable("DB_PASSWORD");
            connString = $"Data Source={DATA_SOURCE};Initial Catalog={INITIAL_CATALOG};User id={DB_USER_ID};Password={DB_PASSWORD};Connect Timeout=300;";
            personProcessor = ProcessorFactory.CreatePersonProcessor(connString);
            noteProcessor = ProcessorFactory.CreateNoteProcessor(connString);

        }
        // GET: api/<NoteController>/2
        [HttpGet("GetByPerson")]
        [Authorize]
        public IActionResult GetByPerson()
        {
            Person person = GetCurrentUser();

            if (person == null || person.ID == Guid.Parse("00000000-0000-0000-0000-000000000000"))
            {
                return NotFound("User not found.");
            }

            List<Note> notes = noteProcessor.GetByPerson(person.ID);

            return Ok(notes);
        }

        // GET api/<NoteController>/5
        [HttpGet("{_id}")]
        [Authorize]
        public APIResponse Get(Guid _id)
        {
            APIResponse response = new(APIResponseStatus.Success);

            if (!noteProcessor.DoesNoteExits(_id))
            {
                response.Status = APIResponseStatus.Failure;
                response.Message = "Note does not exist.";
                return response;
            }

            Note note = noteProcessor.GetById(_id);

            response.ResponseBody = note;
            return response;
        }

        // POST api/<NoteController>
        [HttpPost]
        [Authorize]
        public APIResponse Post([FromBody] NoteRequestParams _note)
        {
            APIResponse response = new(APIResponseStatus.Success);

            if (!noteProcessor.IsValidTitle(_note.Title))
            {
                response.Status = APIResponseStatus.Failure;
                response.Message = "Title cannot be empty.";
                return response;
            }
            if (!noteProcessor.IsTitleUnique(_note.Title))
            {
                response.Status = APIResponseStatus.Failure;
                response.Message = "Note with this title arleady exists.";
                return response;
            }
            if (!noteProcessor.IsValidText(_note.Text))
            {
                response.Status = APIResponseStatus.Failure;
                response.Message = "Text cannot be empty.";
                return response;
            }
            if (_note.CategoryId == Guid.Parse("00000000-0000-0000-0000-000000000000"))
            {
                response.Status = APIResponseStatus.Failure;
                response.Message = "No valid CategoryId.";
                return response;
            }
            if (_note.PersonId == Guid.Parse("00000000-0000-0000-0000-000000000000"))
            {
                response.Status = APIResponseStatus.Failure;
                response.Message = "No valid PersonId.";
                return response;
            }

            Note note = noteProcessor.Create(_note.Title, _note.Text, _note.CategoryId, _note.PersonId);

            if (note.ID == Guid.Parse("00000000-0000-0000-0000-000000000000"))
            {
                response.Status = APIResponseStatus.Failure;
                response.Message = "Note could not be created.";
                return response;
            }

            response.ResponseBody = note;
            return response;
        }

        // PUT api/<NoteController>/5
        [HttpPut("{_id}")]
        [Authorize]
        public APIResponse Put(Guid _id, [FromBody] NoteRequestParams _note)
        {
            APIResponse response = new(APIResponseStatus.Success);

            if (!noteProcessor.DoesNoteExits(_note.ID))
            {
                response.Status = APIResponseStatus.Failure;
                response.Message = "Note does not exist.";
                return response;
            }
            if (!noteProcessor.IsValidTitle(_note.Title))
            {
                response.Status = APIResponseStatus.Failure;
                response.Message = "Title cannot be empty.";
                return response;
            }
            if (!noteProcessor.IsTitleUnique(_note.Title))
            {
                response.Status = APIResponseStatus.Failure;
                response.Message = "Note with this title arleady exists.";
                return response;
            }
            if (!noteProcessor.IsValidText(_note.Text))
            {
                response.Status = APIResponseStatus.Failure;
                response.Message = "Text cannot be empty.";
                return response;
            }
            if (_note.CategoryId == Guid.Parse("00000000-0000-0000-0000-000000000000"))
            {
                response.Status = APIResponseStatus.Failure;
                response.Message = "No valid CategoryId.";
                return response;
            }
            if (_note.PersonId == Guid.Parse("00000000-0000-0000-0000-000000000000"))
            {
                response.Status = APIResponseStatus.Failure;
                response.Message = "No valid PersonId.";
                return response;
            }

            //retrieve note first to get the tags
            Note note = noteProcessor.GetById(_id);
            note = noteProcessor.Update(_note.ID, _note.Title, _note.Text, _note.CategoryId, _note.PersonId, note.tagList);

            response.ResponseBody = note;
            return response;
        }

        // DELETE api/<NoteController>/5
        [HttpDelete("{_id}")]
        [Authorize]
        public APIResponse Delete(Guid _id)
        {
            APIResponse response = new(APIResponseStatus.Success);

            Note note = noteProcessor.GetById(_id);

            if (note.ID == Guid.Parse("00000000-0000-0000-0000-000000000000"))
            {
                response.Status = APIResponseStatus.Failure;
                response.Message = "Note does not exist.";
                return response;
            }

            noteProcessor.Delete(note.ID, note.tagList, note.PersonId);

            return response;
        }

        private Person GetCurrentUser()
        {
            ClaimsIdentity? identity = HttpContext.User.Identity as ClaimsIdentity;

            if (identity == null || identity.Claims.Count() <= 0)
            {
                return null;
            }

            IEnumerable<Claim> userClaims = identity.Claims;

            return personProcessor.GetByEmail(userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Email)?.Value);
        }
    }
}
