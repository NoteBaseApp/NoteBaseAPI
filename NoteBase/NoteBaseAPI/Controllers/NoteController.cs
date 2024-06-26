﻿using Microsoft.AspNetCore.Authorization;
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
        private readonly ITagProcessor tagProcessor;
        private readonly ICategoryProcessor categoryProcessor;

        public NoteController() 
        {
            string? DATA_SOURCE = Environment.GetEnvironmentVariable("DATA_SOURCE");
            string? INITIAL_CATALOG = Environment.GetEnvironmentVariable("INITIAL_CATALOG");
            string? DB_USER_ID = Environment.GetEnvironmentVariable("DB_USER_ID");
            string? DB_PASSWORD = Environment.GetEnvironmentVariable("DB_PASSWORD");
            connString = $"Data Source={DATA_SOURCE};Initial Catalog={INITIAL_CATALOG};User id={DB_USER_ID};Password={DB_PASSWORD};Connect Timeout=300;";
            personProcessor = ProcessorFactory.CreatePersonProcessor(connString);
            noteProcessor = ProcessorFactory.CreateNoteProcessor(connString);
            tagProcessor = ProcessorFactory.CreateTagProcessor(connString);
            categoryProcessor = ProcessorFactory.CreateCategoryProcessor(connString);
        }

        [HttpGet("GetByPerson")]
        [Authorize]
        public IActionResult GetByPerson()
        {
            Person? person = GetCurrentUser();

            //should be obsolete because it already gets checkt when creating the jwt accestoken
            /* if (person == null || person.ID == Guid.Parse("00000000-0000-0000-0000-000000000000"))
            {
                return NotFound(new Error("DoesNotExist", "User does not exist."));
            } */

            List<Note> notes = noteProcessor.GetByPerson(person.ID);

            return Ok(notes);
        }

        [HttpGet("GetByTag/{_tagId}")]
        [Authorize]
        public IActionResult GetByTag(Guid _tagId)
        {
            Person? person = GetCurrentUser();

            if (!tagProcessor.DoesTagExits(_tagId))
            {
                return NotFound(new Error("DoesNotExist", "Tag does not exist."));
            }

            List<Note> notes = noteProcessor.GetByTag(_tagId, person.ID);

            return Ok(notes);
        }

        [HttpGet("{_id}")]
        [Authorize]
        public IActionResult Get(Guid _id)
        {
            Person? person = GetCurrentUser();

            if (!noteProcessor.DoesNoteExits(_id))
            {
                return NotFound(new Error("DoesNotExist", "Note does not exist."));
            }

            Note note = noteProcessor.GetById(_id);

            if (note.PersonId != person.ID)
            {
                return Forbid();
            }

            return Ok(note);
        }

        [HttpPost]
        [Authorize]
        public IActionResult Post([FromBody] NoteRequestParams _note)
        {
            Person? person = GetCurrentUser();

            if (!noteProcessor.IsValidTitle(_note.Title))
            {
                return BadRequest(new Error("NoValidTitle", "Title cannot be empty."));
            }
            if (!noteProcessor.IsTitleUnique(_note.Title))
            {
                return BadRequest(new Error("AlreadyExists", "Note with this title arleady exists."));
            }
            if (!noteProcessor.IsValidText(_note.Text))
            {
                return BadRequest(new Error("NoValidText", "Text cannot be empty."));
            }
            if (_note.CategoryId == Guid.Parse("00000000-0000-0000-0000-000000000000"))
            {
                return BadRequest(new Error("NoValidId", "No valid CategoryId."));
            }

            Category category = categoryProcessor.GetById(_note.CategoryId);

            if (category.PersonId != person.ID)
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

            if (!noteProcessor.DoesNoteExits(_note.ID))
            {
                return NotFound(new Error("DoesNotExist", "Note does not exist."));
            }

            //retrieve note first to get the tags (and for ownership check)
            Note note = noteProcessor.GetById(_note.ID);

            if (note.PersonId != person.ID)
            {
                return Forbid();
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

            Category category = categoryProcessor.GetById(_note.CategoryId);

            if (category.PersonId != person.ID)
            {
                return BadRequest(new Error("NoValidId", "No valid CategoryId."));
            }

            note = noteProcessor.Update(_note.ID, _note.Title, _note.Text, _note.CategoryId, person.ID, note.tagList);

            return Ok(note);
        }

        [HttpDelete("{_id}")]
        [Authorize]
        public IActionResult Delete(Guid _id)
        {
            Person? person = GetCurrentUser();

            Note note = noteProcessor.GetById(_id);

            if (note.ID == Guid.Parse("00000000-0000-0000-0000-000000000000"))
            {
                return NotFound(new Error("DoesNotExist", "Note does not exist."));
            }

            if (note.PersonId != person.ID)
            {
                return Forbid();
            }

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
