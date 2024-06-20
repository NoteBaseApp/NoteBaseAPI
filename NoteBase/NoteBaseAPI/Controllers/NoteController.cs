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
        public IActionResult Get(Guid _id)
        {
            if (!noteProcessor.DoesNoteExits(_id))
            {
                return NotFound(new Error("DoesNotExist", "Note does not exist."));
            }

            Note note = noteProcessor.GetById(_id);

            return Ok(note);
        }

        // POST api/<NoteController>
        [HttpPost]
        [Authorize]
        public IActionResult Post([FromBody] NoteRequestParams _note)
        {
            if (!noteProcessor.IsValidTitle(_note.Title))
            {
                return BadRequest(new Error("InValidTitle", "Title cannot be empty."));
            }
            if (!noteProcessor.IsTitleUnique(_note.Title))
            {
                return BadRequest(new Error("AlreadyExists", "Category with this title arleady exists."));
            }
            if (!noteProcessor.IsValidText(_note.Text))
            {
                return BadRequest(new Error("InValidText", "Text cannot be empty."));
            }
            if (_note.CategoryId == Guid.Parse("00000000-0000-0000-0000-000000000000"))
            {
                return BadRequest(new Error("NoValidId", "No valid CategoryId."));
            }
            if (_note.PersonId == Guid.Parse("00000000-0000-0000-0000-000000000000"))
            {
                return BadRequest(new Error("NoValidId", "No valid PersonId."));
            }

            Note note = noteProcessor.Create(_note.Title, _note.Text, _note.CategoryId, _note.PersonId);

            if (note.ID == Guid.Parse("00000000-0000-0000-0000-000000000000"))
            {
                throw new Exception("Category could not be created.");
            }

            return Ok(note);
        }

        // PUT api/<NoteController>/5
        [HttpPut("{_id}")]
        [Authorize]
        public IActionResult Put(Guid _id, [FromBody] NoteRequestParams _note)
        {
            if (!noteProcessor.DoesNoteExits(_note.ID))
            {
                return NotFound(new Error("DoesNotExist", "Note does not exist."));
            }
            if (!noteProcessor.IsTitleUnique(_note.Title))
            {
                return BadRequest(new Error("AlreadyExists", "Category with this title arleady exists."));
            }
            if (!noteProcessor.IsValidText(_note.Text))
            {
                return BadRequest(new Error("InValidText", "Text cannot be empty."));
            }
            if (_note.CategoryId == Guid.Parse("00000000-0000-0000-0000-000000000000"))
            {
                return BadRequest(new Error("NoValidId", "No valid CategoryId."));
            }
            if (_note.PersonId == Guid.Parse("00000000-0000-0000-0000-000000000000"))
            {
                return BadRequest(new Error("NoValidId", "No valid PersonId."));
            }

            //retrieve note first to get the tags
            Note note = noteProcessor.GetById(_id);
            note = noteProcessor.Update(_note.ID, _note.Title, _note.Text, _note.CategoryId, _note.PersonId, note.tagList);

            return Ok(note);
        }

        // DELETE api/<NoteController>/5
        [HttpDelete("{_id}")]
        [Authorize]
        public IActionResult Delete(Guid _id)
        {
            Note note = noteProcessor.GetById(_id);

            if (note.ID == Guid.Parse("00000000-0000-0000-0000-000000000000"))
            {
                return NotFound(new Error("DoesNotExist", "Note does not exist."));
            }

            noteProcessor.Delete(note.ID, note.tagList, note.PersonId);

            return Ok();
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
