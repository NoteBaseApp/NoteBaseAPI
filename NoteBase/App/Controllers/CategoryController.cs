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

        public CategoryController(IConfiguration configuration)
        {
            _config = configuration;
            connString = _config.GetConnectionString("NoteBaseConnString");
            personProcessor = ProcessorFactory.CreatePersonProcessor(connString);
            categoryProcessor = ProcessorFactory.CreateCategoryProcessor(connString);
        }

        // GET: CategoryController/Details/5
        public ActionResult Details(int id)
        {
            INoteProcessor noteProcessor = ProcessorFactory.CreateNoteProcessor(connString);

            Response<Note> noteResponse = noteProcessor.GetByPerson(personProcessor.GetByEmail(User.Identity.Name).Data[0].ID);
            ResponseModel<NoteModel> noteResponseModel = new(noteResponse.Succeeded);
            noteResponseModel.Code = noteResponse.Code;
            noteResponseModel.Message = noteResponse.Message;

            Response<Category> categoryResponse = categoryProcessor.GetById(id);
            ResponseModel<CategoryModel> categoryResponseModel = new(categoryResponse.Succeeded);

            for (int i = 0; i < 10; i++)
            {
                categoryResponse = categoryProcessor.GetById(id);
                categoryResponseModel = new(categoryResponse.Succeeded)
                {
                    Code = categoryResponse.Code,
                    Message = categoryResponse.Message
                };

                if (noteResponseModel.Data.Count > 0 &&
                    categoryResponseModel.Data.Count > 0)
                {
                    break;
                }

                Thread.Sleep(50);
            }

            if (noteResponse.Data.Count > 0)
            {
                foreach (Note note in noteResponse.Data)
                {
                    NoteModel noteModel = new(note.Title, note.Text,
                        new(note.Category.Title) { 
                            ID = note.Category.ID
                        })
                    {
                        ID = note.ID
                    };

                    foreach (Tag tag in note.TagList)
                    {
                        noteModel.AddTag(new(tag.Title)
                        {
                            ID = tag.ID
                        });
                    }

                    noteResponseModel.AddItem(noteModel);
                }
            }

            if (categoryResponse.Data.Count > 0)
            {
                categoryResponseModel.AddItem(new(categoryResponse.Data[0].Title)
                {
                    ID = categoryResponse.Data[0].ID,
                    PersonId = categoryResponse.Data[0].PersonId
                });
            }

            dynamic data = new ExpandoObject();
            data.noteResponseModel = noteResponseModel;
            data.categoryresponseModel = categoryResponseModel;

            return View(data);
        }

        // GET: CategoryController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CategoryController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                CategoryModel categoryModel = new(collection["Title"])
                {
                    PersonId = personProcessor.GetByEmail(User.Identity.Name).Data[0].ID
                };
                Response<Category> response = categoryProcessor.Create(categoryModel.ToLogicModel());

                if (!response.Succeeded)
                {
                    return View();
                }

                //diffrent redirect options? book example
                return RedirectToAction(nameof(Details), response.Data[0].ID);
            }
            catch
            {
                return View();
            }
        }

        // GET: CategoryController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CategoryController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Details));
            }
            catch
            {
                return View();
            }
        }

        // GET: CategoryController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CategoryController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Details));
            }
            catch
            {
                return View();
            }
        }
    }
}
