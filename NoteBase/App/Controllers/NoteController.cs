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
            try
            {
                Response<Note> noteResponse = noteProcessor.GetById(id);

                if (!noteResponse.Succeeded || noteResponse.Data.Count == 0)
                {
                    ViewBag.Succeeded = noteResponse.Succeeded;
                    ViewBag.Message = noteResponse.Message;
                    ViewBag.Code = noteResponse.Code;

                    return View();
                }

                ViewBag.Succeeded = noteResponse.Succeeded;

                return View(new NoteModel(noteResponse.Data[0]));
            }
            catch (Exception e)
            {
                ViewBag.Succeeded = false;
                ViewBag.Message = e.Message;
                return View();
            }
        }

        // GET: Note/Create
        public ActionResult Create()
        {
            try
            {
                Response<Person> personResponse = personProcessor.GetByEmail(User.Identity.Name);

                if (!personResponse.Succeeded)
                {
                    ViewBag.Succeeded = personResponse.Succeeded;
                    ViewBag.Message = personResponse.Message;
                    ViewBag.Code = personResponse.Code;

                    return View();
                }

                person = personResponse.Data[0];

                Response<Category> categoryResponse = categoryProcessor.GetByPerson(person.ID);

                if (!categoryResponse.Succeeded)
                {
                    ViewBag.Succeeded = categoryResponse.Succeeded;
                    ViewBag.Message = categoryResponse.Message;
                    ViewBag.Code = categoryResponse.Code;

                    return View();
                }

                List<CategoryModel> categorymodellist = new();
                foreach (Category category in categoryResponse.Data)
                {
                    categorymodellist.Add(new(category));
                }

                ViewBag.CategoryList = categorymodellist;
                ViewBag.Succeeded = categoryResponse.Succeeded;

                return View();
            }
            catch (Exception e)
            {
                ViewBag.Succeeded = false;
                ViewBag.Message = e.Message;
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
                Response<Person> personResponse = personProcessor.GetByEmail(User.Identity.Name);

                if (!personResponse.Succeeded)
                {
                    ViewBag.Succeeded = personResponse.Succeeded;
                    ViewBag.Message = personResponse.Message;
                    ViewBag.Code = personResponse.Code;

                    return View();
                }

                person = personResponse.Data[0];

                NoteModel NoteModel = new(0, collection["Title"], collection["Text"], Int32.Parse(collection["CategoryId"]));
                NoteModel.PersonId = person.ID;
                Response<Note> noteResponse = noteProcessor.Create(NoteModel.ToLogicModel());

                if (!noteResponse.Succeeded)
                {
                    ViewBag.Succeeded = noteResponse.Succeeded;
                    ViewBag.Message = noteResponse.Message;
                    ViewBag.Code = noteResponse.Code;

                    return View();
                }

                Response<Category> categoryResponse = categoryProcessor.GetByPerson(person.ID);

                if (!categoryResponse.Succeeded)
                {
                    ViewBag.Succeeded = categoryResponse.Succeeded;
                    ViewBag.Message = categoryResponse.Message;
                    ViewBag.Code = categoryResponse.Code;

                    return View();
                }

                List<CategoryModel> categorymodellist = new();
                foreach (Category category in categoryResponse.Data)
                {
                    categorymodellist.Add(new(category));
                }

                ViewBag.CategoryList = categorymodellist;
                ViewBag.Succeeded = categoryResponse.Succeeded;

                //diffrent redirect options? book example
                return RedirectToAction(nameof(Details), noteResponse.Data[0].ID);
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
            try 
            {
                Response<Person> personResponse = personProcessor.GetByEmail(User.Identity.Name);

                if (!personResponse.Succeeded)
                {
                    ViewBag.Succeeded = personResponse.Succeeded;
                    ViewBag.Message = personResponse.Message;
                    ViewBag.Code = personResponse.Code;

                    return View();
                }

                person = personResponse.Data[0];

                Response<Note> noteResponse = noteProcessor.GetById(id);

                if (!noteResponse.Succeeded)
                {
                    ViewBag.Succeeded = noteResponse.Succeeded;
                    ViewBag.Message = noteResponse.Message;
                    ViewBag.Code = noteResponse.Code;

                    return View();
                }

                ResponseModel<NoteModel> noteModelResponse = new(noteResponse.Succeeded);
                noteModelResponse.AddItem(new NoteModel(noteResponse.Data[0].ID, noteResponse.Data[0].Title, noteResponse.Data[0].Text, noteResponse.Data[0].CategoryId));

                Response<Category> categoryResponse = categoryProcessor.GetByPerson(person.ID);

                if (!categoryResponse.Succeeded)
                {
                    ViewBag.Succeeded = categoryResponse.Succeeded;
                    ViewBag.Message = categoryResponse.Message;
                    ViewBag.Code = categoryResponse.Code;

                    return View();
                }

                List<CategoryModel> categorymodellist = new();
                foreach (Category category in categoryResponse.Data)
                {
                    categorymodellist.Add(new(category));
                }

                ViewBag.CategoryList = categorymodellist;
                ViewBag.Succeeded = categoryResponse.Succeeded;

                return View(noteModelResponse.Data[0]);

            }
            catch (Exception e)
            {
                ViewBag.Succeeded = false;
                ViewBag.Message = e.Message;
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
                Response<Person> personResponse = personProcessor.GetByEmail(User.Identity.Name);

                if (!personResponse.Succeeded)
                {
                    ViewBag.Succeeded = personResponse.Succeeded;
                    ViewBag.Message = personResponse.Message;
                    ViewBag.Code = personResponse.Code;

                    return View();
                }

                person = personResponse.Data[0];

                NoteModel noteModel = new(id, collection["Title"], collection["Text"], Int32.Parse(collection["CategoryId"]));
                Response<Note> noteResponse = noteProcessor.Update(noteModel.ToLogicModel());

                if (!noteResponse.Succeeded)
                {
                    ViewBag.Succeeded = noteResponse.Succeeded;
                    ViewBag.Message = noteResponse.Message;
                    ViewBag.Code = noteResponse.Code;

                    return View();
                }

                Response<Category> categoryResponse = categoryProcessor.GetByPerson(person.ID);

                if (!categoryResponse.Succeeded)
                {
                    ViewBag.Succeeded = categoryResponse.Succeeded;
                    ViewBag.Message = categoryResponse.Message;
                    ViewBag.Code = categoryResponse.Code;

                    return View();
                }

                List<CategoryModel> categorymodellist = new();
                foreach (Category category in categoryResponse.Data)
                {
                    categorymodellist.Add(new(category));
                }

                ViewBag.CategoryList = categorymodellist;
                ViewBag.Succeeded = categoryResponse.Succeeded;

                //diffrent redirect options?
                return RedirectToAction(nameof(Details), noteResponse.Data[0].ID);
            }
            catch (Exception e)
            {
                ViewBag.Succeeded = false;
                ViewBag.Message = e.Message;
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
                Response<Person> personResponse = personProcessor.GetByEmail(User.Identity.Name);

                if (!personResponse.Succeeded)
                {
                    ViewBag.Succeeded = personResponse.Succeeded;
                    ViewBag.Message = personResponse.Message;
                    ViewBag.Code = personResponse.Code;

                    return View();
                }

                person = personResponse.Data[0];

                Response<Note> noteResponse = noteProcessor.GetById(id);

                if (!noteResponse.Succeeded)
                {
                    ViewBag.Succeeded = noteResponse.Succeeded;
                    ViewBag.Message = noteResponse.Message;
                    ViewBag.Code = noteResponse.Code;

                    return View();
                }

                noteResponse = noteProcessor.Delete(noteResponse.Data[0], person.ID);

                ViewBag.Succeeded = noteResponse.Succeeded;
                ViewBag.Message = noteResponse.Message;
                ViewBag.Code = noteResponse.Code;

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
