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

            noteResponseModel.AddItem(new(noteResponse.Data[0]));

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
                categorymodellist.Add(new(category));
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
            Response<Person> personResponse = personProcessor.GetByEmail(User.Identity.Name);
            person = personResponse.Data[0];

            Response<Note> noteResponse = noteProcessor.GetById(id);

            ResponseModel<NoteModel> noteModelResponse = new(noteResponse.Succeeded);
            noteModelResponse.AddItem(new NoteModel(noteResponse.Data[0].ID, noteResponse.Data[0].Title, noteResponse.Data[0].Text, noteResponse.Data[0].CategoryId));

            Response<Category> categoryResponse = categoryProcessor.GetByPerson(person.ID);
            List<CategoryModel> categorymodellist = new();

            foreach (Category category in categoryResponse.Data)
            {
                categorymodellist.Add(new(category));
            }
            ViewBag.CategoryList = categorymodellist;

            if (!noteResponse.Succeeded)
            {
                ViewBag.Succeeded = noteResponse.Succeeded;
                ViewBag.Message = noteResponse.Message;
                ViewBag.Code = noteResponse.Code;

                return View();
            }

            return View(noteModelResponse.Data[0]);

        }

        // POST: Note/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            Response<Person> personResponse = personProcessor.GetByEmail(User.Identity.Name);
            person = personResponse.Data[0];

            try
            {
                NoteModel noteModel = new(id, collection["Title"], collection["Text"], Int32.Parse(collection["CategoryId"]));
                Response<Note> response = noteProcessor.Update(noteModel.ToLogicModel());

                Response<Category> categoryResponse = categoryProcessor.GetByPerson(person.ID);
                List<CategoryModel> categorymodellist = new();

                foreach (Category category in categoryResponse.Data)
                {
                    categorymodellist.Add(new(category));
                }
                ViewBag.CategoryList = categorymodellist;

                if (!response.Succeeded)
                {
                    ViewBag.Succeeded = response.Succeeded;
                    ViewBag.Message = response.Message;
                    ViewBag.Code = response.Code;

                    return View();
                }

                //diffrent redirect options?
                return RedirectToAction(nameof(Details), response.Data[0].ID);
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
            Response<Person> personResponse = personProcessor.GetByEmail(User.Identity.Name);
            person = personResponse.Data[0];

            ViewBag.Post = true;
            try
            {
                Response<Note> response = noteProcessor.GetById(id);

                if (!response.Succeeded)
                {
                    ViewBag.Succeeded = response.Succeeded;
                    ViewBag.Message = response.Message;
                    ViewBag.Code = response.Code;

                    return View();
                }

                response = noteProcessor.Delete(response.Data[0], person.ID);

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
