using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NoteBaseAPI.Models;
using NoteBaseLogicFactory;
using NoteBaseLogicInterface;
using NoteBaseLogicInterface.Models;
using System.Security.Claims;

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

        [HttpGet("GetByPerson")]
        [Authorize]
        public IActionResult GetByPerson()
        {
            Person? person = GetCurrentUser();

            if (person == null || person.ID == Guid.Parse("00000000-0000-0000-0000-000000000000"))
            {
                return NotFound(new Error("DoesNotExist", "User does not exist."));
            }

            List<Note> notes = noteProcessor.GetByPerson(person.ID);

            return Ok(notes);
        }

        [HttpGet("{_id}")]
        [Authorize]
        public IActionResult Get(Guid _id)
        {
            if (!noteProcessor.DoesNoteExits(_id))
            {
                return NotFound(new Error("DoesNotExist", "Note does not exist."));
            }

            //add ownership check

            Note note = noteProcessor.GetById(_id);

            return Ok(note);
        }

        [HttpPost]
        [Authorize]
        public IActionResult Post([FromBody] NoteRequestParams _note)
        {
            Person? person = GetCurrentUser();

            if (person == null || person.ID == Guid.Parse("00000000-0000-0000-0000-000000000000"))
            {
                return NotFound(new Error("DoesNotExist", "User does not exist."));
            }

            if (!noteProcessor.IsValidTitle(_note.Title))
            {
                return BadRequest(new Error("NoValidTitle", "Title cannot be empty."));
            }
            if (!noteProcessor.IsTitleUnique(_note.Title))
            {
                return BadRequest(new Error("AlreadyExists", "Category with this title arleady exists."));
            }
            if (!noteProcessor.IsValidText(_note.Text))
            {
                return BadRequest(new Error("NoValidText", "Text cannot be empty."));
            }
            if (_note.CategoryId == Guid.Parse("00000000-0000-0000-0000-000000000000"))
            {
                return BadRequest(new Error("NoValidId", "No valid CategoryId."));
            }

            Note note = noteProcessor.Create(_note.Title, _note.Text, _note.CategoryId, person.ID);

            if (note.ID == Guid.Parse("00000000-0000-0000-0000-000000000000"))
            {
                throw new Exception("Category could not be created.");
            }

            return Ok(note);
        }

        [HttpPut]
        [Authorize]
        public IActionResult Put([FromBody] NoteRequestParams _note)
        {
            Person? person = GetCurrentUser();

            if (person == null || person.ID == Guid.Parse("00000000-0000-0000-0000-000000000000"))
            {
                return NotFound(new Error("DoesNotExist", "User does not exist."));
            }

            if (_note.ID == Guid.Parse("00000000-0000-0000-0000-000000000000"))
            {
                return BadRequest(new Error("NoValidId", "No valid ID"));
            }

            if (!noteProcessor.DoesNoteExits(_note.ID))
            {
                return NotFound(new Error("DoesNotExist", "Note does not exist."));
            }

            //add ownership check

            if (!noteProcessor.IsTitleUnique(_note.Title))
            {
                return BadRequest(new Error("AlreadyExists", "Category with this title arleady exists."));
            }
            if (!noteProcessor.IsValidText(_note.Text))
            {
                return BadRequest(new Error("NoValidText", "Text cannot be empty."));
            }
            if (_note.CategoryId == Guid.Parse("00000000-0000-0000-0000-000000000000"))
            {
                return BadRequest(new Error("NoValidId", "No valid CategoryId."));
            }

            //retrieve note first to get the tags
            Note note = noteProcessor.GetById(_note.ID);
            note = noteProcessor.Update(_note.ID, _note.Title, _note.Text, _note.CategoryId, person.ID, note.tagList);

            return Ok(note);
        }

        [HttpDelete("{_id}")]
        [Authorize]
        public IActionResult Delete(Guid _id)
        {
            Note note = noteProcessor.GetById(_id);

            if (note.ID == Guid.Parse("00000000-0000-0000-0000-000000000000"))
            {
                return NotFound(new Error("DoesNotExist", "Note does not exist."));
            }

            //add ownership check

            noteProcessor.Delete(note.ID, note.tagList, note.PersonId);

            return Ok();
        }

        private Person? GetCurrentUser()
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
