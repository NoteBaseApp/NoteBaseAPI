using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NoteBaseLogicFactory;
using NoteBaseLogicInterface.Models;
using NoteBaseLogicInterface;
using System.Configuration;
using App.Models;
using Microsoft.AspNetCore.Authorization;
using System.Data.Common;

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
            Response<Category> categoryResponse = categoryProcessor.GetById(id);

            ResponseModel<CategoryModel> responseModel = new(categoryResponse.Succeeded);
            responseModel.Code = categoryResponse.Code;
            responseModel.Message = categoryResponse.Message;

            if (categoryResponse.Data.Count > 0)
            {
                responseModel.AddItem(new(categoryResponse.Data[0].Title)
                {
                    ID = categoryResponse.Data[0].ID,
                    PersonId = categoryResponse.Data[0].PersonId
                });
            }

            return View(responseModel);
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
