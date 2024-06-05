using Microsoft.AspNetCore.Mvc;
using NoteBaseAPI.Models;
using NoteBaseLogic;
using NoteBaseLogicFactory;
using NoteBaseLogicInterface;
using NoteBaseLogicInterface.Models;
using System;
using System.Net;
using System.Security.Cryptography;
using System.Xml.Linq;
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
            connString = Environment.GetEnvironmentVariable("DATABASE_URL");
            personProcessor = ProcessorFactory.CreatePersonProcessor(connString);
            noteProcessor = ProcessorFactory.CreateNoteProcessor(connString);

        }
        // GET: api/<NoteController>/2
        [HttpGet("GetByPerson/{_personId}")]
        public APIResponse GetByPerson(int _personId)
        {
            APIResponse response = new(APIResponseStatus.Success);

            List<Note> notes = noteProcessor.GetByPerson(_personId);

            if (notes == null || notes.Count == 0)
            {

                response.Message = "No notes where found.";
                return response;
            }

            response.ResponseBody = notes;
            return response;
        }

        // GET api/<NoteController>/5
        [HttpGet("{_id}")]
        public APIResponse Get(int _id)
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
            if (_note.CategoryId == 0)
            {
                response.Status = APIResponseStatus.Failure;
                response.Message = "No valid CategoryId.";
                return response;
            }
            if (_note.PersonId == 0)
            {
                response.Status = APIResponseStatus.Failure;
                response.Message = "No valid PersonId.";
                return response;
            }

            Note note = noteProcessor.Create(_note.Title, _note.Text, _note.CategoryId, _note.PersonId);

            if (note.ID == 0)
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
        public APIResponse Put(int _id, [FromBody] NoteRequestParams _note)
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
            if (_note.CategoryId == 0)
            {
                response.Status = APIResponseStatus.Failure;
                response.Message = "No valid CategoryId.";
                return response;
            }
            if (_note.PersonId == 0)
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
        public APIResponse Delete(int _id)
        {
            APIResponse response = new(APIResponseStatus.Success);

            Note note = noteProcessor.GetById(_id);

            if (note.ID == 0)
            {
                response.Status = APIResponseStatus.Failure;
                response.Message = "Note does not exist.";
                return response;
            }

            noteProcessor.Delete(note.ID, note.tagList, note.PersonId);

            return response;
        }
    }
}
