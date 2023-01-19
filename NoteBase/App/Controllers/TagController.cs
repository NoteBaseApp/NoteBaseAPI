using Microsoft.AspNetCore.Mvc;
using NoteBaseLogicFactory;
using NoteBaseLogicInterface.Models;
using NoteBaseLogicInterface;
using NoteBaseInterface;
using App.Models;
using Microsoft.AspNetCore.Authorization;

namespace App.Controllers
{
    [Authorize]
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
                Tag tag = tagProcessor.GetById(id);

                ViewBag.Tag = tag;

                List<Note> notes = noteProcessor.GetByTag(id);

                List<NoteModel> noteModels = new();

                foreach (Note note in notes)
                {
                    noteModels.Add(new NoteModel(note));
                }

                ViewBag.Succeeded = true;
                return View(noteModels);
            }
            catch (Exception)
            {
                ViewBag.Succeeded = false;
                return View();
            }
        }
    }
}
