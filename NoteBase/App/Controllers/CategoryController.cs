using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NoteBaseLogicFactory;
using NoteBaseLogicInterface.Models;
using NoteBaseLogicInterface;
using System.Configuration;
using App.Models;
using Microsoft.AspNetCore.Authorization;
using System.Data.Common;
using System;
using System.Dynamic;

namespace App.Controllers
{
    [Authorize]
    public class CategoryController : Controller
    {
        private readonly IConfiguration _config;
        private readonly string connString;
        private readonly IPersonProcessor personProcessor;
        private readonly ICategoryProcessor categoryProcessor;
        private Person? person;

        public CategoryController(IConfiguration configuration)
        {
            _config = configuration;
            connString = _config.GetConnectionString("NoteBaseConnString");
            personProcessor = ProcessorFactory.CreatePersonProcessor(connString);
            categoryProcessor = ProcessorFactory.CreateCategoryProcessor(connString);
        }

        // GET: Category/Details/5
        public ActionResult Details(int id)
        {
            Response<Category> categoryResponse = categoryProcessor.GetById(id);
            ResponseModel<CategoryModel> categoryModelResponse = new(categoryResponse.Succeeded);

            for (int i = 0; i < 10; i++)
            {
                categoryResponse = categoryProcessor.GetById(id);
                categoryModelResponse = new(categoryResponse.Succeeded)
                {
                    Code = categoryResponse.Code,
                    Message = categoryResponse.Message
                };

                if (categoryModelResponse.Data.Count > 0)
                {
                    break;
                }

                Thread.Sleep(50);
            }

            if (categoryResponse.Data.Count == 0)
            {
                categoryModelResponse.Succeeded = false;
                return View(categoryModelResponse);
            }

            Category category = categoryResponse.Data[0];
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

            categoryModelResponse.AddItem(categoryModel);

            return View(categoryModelResponse);

        }

        // GET: Category/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Category/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            Response<Person> personResponse = personProcessor.GetByEmail(User.Identity.Name);
            person = personResponse.Data[0];

            try
            {
                CategoryModel categoryModel = new(0, collection["Title"], person.ID);
                Response<Category> response = categoryProcessor.Create(categoryModel.ToLogicModel());

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

        // GET: Category/Edit/5
        public ActionResult Edit(int id)
        {
            Response<Category> categoryResponse = categoryProcessor.GetById(id);

            if (!categoryResponse.Succeeded)
            {
                ViewBag.Succeeded = categoryResponse.Succeeded;
                ViewBag.Message = categoryResponse.Message;
                ViewBag.Code = categoryResponse.Code;

                return View();
            }

            ResponseModel<CategoryModel> categoryModelResponse = new(categoryResponse.Succeeded);
            categoryModelResponse.AddItem(new CategoryModel(categoryResponse.Data[0].ID, categoryResponse.Data[0].Title, categoryResponse.Data[0].PersonId));

            return View(categoryModelResponse.Data[0]);
        }

        // POST: Category/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                CategoryModel categoryModel = new(id, collection["Title"], personProcessor.GetByEmail(User.Identity.Name).Data[0].ID);
                Response<Category> response = categoryProcessor.Update(categoryModel.ToLogicModel());

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


        // GET: Category/Delete/5
        public ActionResult Delete(int id)
        {
            ViewBag.Post = false;
            return View();
        }

        // POST: Category/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            ViewBag.Post = true;
            try
            {
                Response<Category> response = categoryProcessor.Delete(id);

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
