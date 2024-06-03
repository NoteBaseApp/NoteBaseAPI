using Microsoft.AspNetCore.Mvc;
using NoteBaseAPI.Models;
using NoteBaseLogic;
using NoteBaseLogicFactory;
using NoteBaseLogicInterface;
using NoteBaseLogicInterface.Models;
using System;
using System.Net;
using System.Xml.Linq;
using UI.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NoteBaseAPI.Controllers
{
    [Route("api/note")]
    [ApiController]
    public class NoteController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly string connString;
        private readonly IPersonProcessor personProcessor;
        private readonly INoteProcessor noteProcessor;
        private readonly ICategoryProcessor categoryProcessor;

        public NoteController(IConfiguration configuration) 
        {
            _config = configuration;
            //connString = Environment.GetEnvironmentVariable("DATABASE_URL");
            connString = "Data Source=172.17.0.4,1433;Initial Catalog=NoteBase;User id=NoteBaseAPI;Password=K00kW3kk3r!;Connect Timeout=300;";
            personProcessor = ProcessorFactory.CreatePersonProcessor(connString);
            noteProcessor = ProcessorFactory.CreateNoteProcessor(connString);
            categoryProcessor = ProcessorFactory.CreateCategoryProcessor(connString);

        }
        // GET: api/<NoteController>/2
        [HttpGet("GetByPerson/{_personId}")]
        public List<Note> GetByPerson(int _personId)
        {
            List<Note> notes = noteProcessor.GetByPerson(_personId);

            return notes;
        }

        // GET api/<NoteController>/5
        [HttpGet("{_id}")]
        public Note Get(int _id)
        {
            Note note = noteProcessor.GetById(_id);


            if (note.ID == 0)
            {
                throw new HttpRequestException("Note not found.");
            }

            return note;
        }

        // POST api/<NoteController>
        [HttpPost]
        public Note Post([FromBody] NoteRequestParams _note)
        {
            if (!noteProcessor.IsValidTitle(_note.Title))
            {
                throw new HttpRequestException("Title cannot be empty.");
            }
            if (!noteProcessor.IsTitleUnique(_note.Title))
            {
                throw new HttpRequestException("Note with this title arleady exists.");
            }
            if (!noteProcessor.IsValidText(_note.Text))
            {
                throw new HttpRequestException("Text cannot be empty");
            }
            if (_note.CategoryId == 0)
            {
                throw new HttpRequestException("CategoryId cannot be empty");
            }
            if (_note.PersonId == 0)
            {
                throw new HttpRequestException("PersonId cannot be empty");
            }

            Note note = noteProcessor.Create(_note.Title, _note.Text, _note.CategoryId, _note.PersonId);

            if (note.ID == 0)
            {
                throw new HttpRequestException("Note could not be created");
            }

            return note;
        }

        // PUT api/<NoteController>/5
        [HttpPut("{_id}")]
        public void Put(int _id, [FromBody] NoteRequestParams _note)
        {
            if (!noteProcessor.DoesNoteExits(_note.ID))
            {
                throw new HttpRequestException("Note Does not exist.");
            }
            if (!noteProcessor.IsValidTitle(_note.Title))
            {
                throw new HttpRequestException("Title cannot be empty.");
            }
            if (!noteProcessor.IsTitleUnique(_note.Title))
            {
                throw new HttpRequestException("Note with this title arleady exists.");
            }
            if (!noteProcessor.IsValidText(_note.Text))
            {
                throw new HttpRequestException("NoteBody cannot be empty");
            }

            //retrieve note first to get the tags
            Note note = noteProcessor.GetById(_id);

            note = noteProcessor.Update(_note.ID, _note.Title, _note.Text, _note.CategoryId, _note.PersonId, note.tagList);

            if (note.ID == 0)
            {
                throw new HttpRequestException("Note Does not exist.");
            }
        }

        // DELETE api/<NoteController>/5
        [HttpDelete("{_id}")]
        public void Delete(int _id)
        {
            Note note = noteProcessor.GetById(_id);

            noteProcessor.Delete(note.ID, note.tagList, note.PersonId);

        }
    }
}
