using App.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NoteBaseLogicFactory;
using NoteBaseLogicInterface;
using NoteBaseLogicInterface.Models;

namespace App.Controllers
{
    public class NoteController : Controller
    {
        private readonly IConfiguration _config;
        private readonly string connString;
        private readonly IPersonProcessor personProcessor;
        private readonly INoteProcessor noteProcessor;
        private readonly ICategoryProcessor categoryProcessor;
        private Person? person;

        public NoteController(IConfiguration configuration)
        {
            _config = configuration;
            connString = _config.GetConnectionString("NoteBaseConnString");
            personProcessor = ProcessorFactory.CreatePersonProcessor(connString);
            noteProcessor = ProcessorFactory.CreateNoteProcessor(connString);
            categoryProcessor = ProcessorFactory.CreateCategoryProcessor(connString);
        }

        // GET: Note/Details/5
        public ActionResult Details(int id)
        {

            Response<Note> noteResponse = noteProcessor.GetById(id);
            ResponseModel<NoteModel> noteResponseModel = new(noteResponse.Succeeded)
            {
                Message = noteResponse.Message,
                Code = noteResponse.Code
            };

            if (noteResponse.Data.Count == 0)
            {
                noteResponseModel.Succeeded = false;
                return View(noteResponseModel);
            }

            NoteModel noteModel = new(noteResponse.Data[0].ID, noteResponse.Data[0].Title, noteResponse.Data[0].Text, noteResponse.Data[0].CategoryId);

            foreach (Tag tag in noteResponse.Data[0].TagList)
            {
                TagModel tagModel = new(tag.ID, tag.Title);

                noteModel.AddTag(tagModel);
            }

            noteResponseModel.AddItem(noteModel);

            return View(noteResponseModel);
        }

        // GET: Note/Create
        public ActionResult Create()
        {
            Response<Person> personResponse = personProcessor.GetByEmail(User.Identity.Name);
            person = personResponse.Data[0];

            Response<Category> categoryResponse = categoryProcessor.GetByPerson(person.ID);
            List<CategoryModel> categorymodellist = new();

            foreach (Category category in categoryResponse.Data)
            {
                category.FillNoteList(ProcessorFactory.CreateNoteProcessor(connString));
                CategoryModel categoryModel = new(category.ID, category.Title, category.PersonId);

                foreach (Note note in category.NoteList)
                {
                    NoteModel noteModel = new(note.ID, note.Title, note.Text, note.CategoryId);

                    foreach (Tag tag in note.TagList)
                    {
                        TagModel tagModel = new(tag.ID, tag.Title);

                        noteModel.AddTag(tagModel);
                    }

                    categoryModel.AddNote(noteModel);
                }

                categorymodellist.Add(categoryModel);
            }
            ViewBag.CategoryList = categorymodellist;

            return View();
        }

        // POST: Note/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            Response<Person> personResponse = personProcessor.GetByEmail(User.Identity.Name);
            person = personResponse.Data[0];

            try
            {
                NoteModel NoteModel = new(0, collection["Title"], collection["Text"], Int32.Parse(collection["CategoryId"]));
                NoteModel.PersonId = person.ID;
                Response<Note> response = noteProcessor.Create(NoteModel.ToLogicModel());

                if (!response.Succeeded)
                {
                    ViewBag.Succeeded = response.Succeeded;
                    ViewBag.Message = response.Message;
                    ViewBag.Code = response.Code;

                    return View();
                }

                //diffrent redirect options? book example
                return RedirectToAction(nameof(Details), response.Data[0].ID);
            }
            catch(Exception e)
            {
                ViewBag.Succeeded = false;
                ViewBag.Message = e.Message;
                return View();
            }
        }

        // GET: Note/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Note/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Note/Delete/5
        public ActionResult Delete(int id)
        {
            ViewBag.Post = false;
            return View();
        }

        // POST: Note/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            ViewBag.Post = true;
            try
            {
                Response<Note> response = noteProcessor.Delete(id);

                ViewBag.Succeeded = response.Succeeded;
                ViewBag.Message = response.Message;
                ViewBag.Code = response.Code;

                return View();
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
