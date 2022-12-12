using Microsoft.AspNetCore.Mvc;
using NoteBaseLogicFactory;
using NoteBaseLogicInterface.Models;
using NoteBaseLogicInterface;
using NoteBaseInterface;
using App.Models;

namespace App.Controllers
{
    public class TagController : Controller
    {
        private readonly IConfiguration _config;
        private readonly string connString;
        private readonly INoteProcessor noteProcessor;
        private readonly ITagProcessor tagProcessor;

        public TagController(IConfiguration configuration)
        {
            _config = configuration;
            connString = _config.GetConnectionString("NoteBaseConnString");
            noteProcessor = ProcessorFactory.CreateNoteProcessor(connString);
            tagProcessor = ProcessorFactory.CreateTagProcessor(connString);
        }

        public IActionResult Index(int id)
        {
            try
            {
                Response<Tag> tagResponse = tagProcessor.GetById(id);

                if (!tagResponse.Succeeded)
                {
                    ViewBag.Succeeded = tagResponse.Succeeded;
                    ViewBag.Message = tagResponse.Message;
                    ViewBag.Code = tagResponse.Code;

                    return View();
                }

                ViewBag.Tag = tagResponse.Data[0];

                Response<Note> noteResponse = noteProcessor.GetByTag(id);

                if (!noteResponse.Succeeded)
                {
                    ViewBag.Succeeded = noteResponse.Succeeded;
                    ViewBag.Message = noteResponse.Message;
                    ViewBag.Code = noteResponse.Code;

                    return View();
                }

                List<NoteModel> notes = new();

                foreach (Note note in noteResponse.Data)
                {
                    notes.Add(new NoteModel(note));
                }

                ViewBag.Succeeded = noteResponse.Succeeded;
                return View(notes);
            }
            catch (Exception e)
            {
                ViewBag.Succeeded = false;
                ViewBag.Message = e.Message;
                return View();
            }
        }
    }
}
