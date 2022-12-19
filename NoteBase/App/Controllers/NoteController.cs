using App.Models;
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
            try
            {
                Note note = noteProcessor.GetById(id);


                if (note.ID == 0)
                {
                    ViewBag.Succeeded = false;
                    ViewBag.Message = "Notitie niet gevonden";

                    return View();
                }

                ViewBag.Succeeded = true;

                return View(new NoteModel(note));
            }
            catch (Exception e)
            {
                ViewBag.Succeeded = false;
                return View();
            }
        }

        // GET: Note/Create
        public ActionResult Create()
        {
            try
            {
                this.person = personProcessor.GetByEmail(User.Identity.Name);

                List<Category> categories = categoryProcessor.GetByPerson(this.person.ID);

                List<CategoryModel> categoryModels = new();
                foreach (Category category in categories)
                {
                    categoryModels.Add(new(category));
                }

                ViewBag.CategoryList = categoryModels;
                ViewBag.Succeeded = true;

                return View();
            }
            catch (Exception e)
            {
                ViewBag.Succeeded = false;
                return View();
            }
        }

        // POST: Note/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                this.person = personProcessor.GetByEmail(User.Identity.Name);

                List<Category> categories = categoryProcessor.GetByPerson(person.ID);

                List<CategoryModel> categoryModels = new();
                foreach (Category category in categories)
                {
                    categoryModels.Add(new(category));
                }
                ViewBag.CategoryList = categoryModels;

                NoteModel NoteModel = new(0, collection["Title"], collection["Text"], Int32.Parse(collection["CategoryId"]));
                NoteModel.PersonId = person.ID;
                Note note = noteProcessor.Create(NoteModel.ToLogicModel());

                if (note.ID == 0)
                {
                    ViewBag.Succeeded = false;
                    return View();
                }

                ViewBag.Succeeded = true;

                //diffrent redirect options? book example
                return RedirectToAction(nameof(Details), note.ID);
            }
            catch(Exception e)
            {
                ViewBag.Succeeded = false;
                return View();
            }
        }

        // GET: Note/Edit/5
        public ActionResult Edit(int id)
        {
            try 
            {
                this.person = personProcessor.GetByEmail(User.Identity.Name);

                Note note = noteProcessor.GetById(id);

                List<Category> categories = categoryProcessor.GetByPerson(person.ID);

                List<CategoryModel> categoryModels = new();
                foreach (Category category in categories)
                {
                    categoryModels.Add(new(category));
                }

                ViewBag.CategoryList = categoryModels;

                if (note.ID == 0 || categoryModels.Count == 0)
                {
                    ViewBag.Succeeded = false;
                    return View();
                }

                ViewBag.Succeeded = true;

                return View(new NoteModel(note));

            }
            catch (Exception e)
            {
                ViewBag.Succeeded = false;
                return View();
            }

        }

        // POST: Note/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                this.person = personProcessor.GetByEmail(User.Identity.Name);

                NoteModel noteModel = new(id, collection["Title"], collection["Text"], Int32.Parse(collection["CategoryId"]));
                Note note = noteProcessor.Update(noteModel.ToLogicModel());

                List<Category> categories = categoryProcessor.GetByPerson(person.ID);

                List<CategoryModel> categoryModels = new();
                foreach (Category category in categories)
                {
                    categoryModels.Add(new(category));
                }

                ViewBag.CategoryList = categoryModels;

                if (note.ID == 0 || categoryModels.Count == 0)
                {
                    ViewBag.Succeeded = false;
                    return View();
                }

                ViewBag.Succeeded = true;

                //diffrent redirect options?
                return RedirectToAction(nameof(Details), note.ID);
            }
            catch (Exception e)
            {
                ViewBag.Succeeded = false;
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
                this.person = personProcessor.GetByEmail(User.Identity.Name);

                Note note = noteProcessor.GetById(id);

                if (note.ID == 0)
                {
                    ViewBag.Succeeded = false;
                    return View();
                }

                int noteDeleteResult = noteProcessor.Delete(note, person.ID);

                if (noteDeleteResult == 0)
                {
                    ViewBag.Succeeded = false;
                    return View();
                }

                ViewBag.Succeeded = true;

                return View();
            }
            catch (Exception e)
            {
                ViewBag.Succeeded = false;
                return View();
            }
        }
    }
}
