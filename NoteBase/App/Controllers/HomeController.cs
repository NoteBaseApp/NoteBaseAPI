using App.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NoteBaseLogicFactory;
using NoteBaseLogicInterface;
using NoteBaseLogicInterface.Models;
using System.Diagnostics;
using System.Dynamic;

namespace App.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _config;
        private readonly string connString;
        private Person ?person;
        private readonly IPersonProcessor personProcessor;


        public HomeController(ILogger<HomeController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _config = configuration;
            connString = _config.GetConnectionString("NoteBaseConnString");

            personProcessor = ProcessorFactory.CreatePersonProcessor(connString);
        }

        [Authorize]
        public IActionResult Index()
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

                ICategoryProcessor categoryProcessor = ProcessorFactory.CreateCategoryProcessor(connString);
                Response<Category> categoryResponse = categoryProcessor.GetByPerson(person.ID);

                if (!categoryResponse.Succeeded)
                {
                    ViewBag.Succeeded = categoryResponse.Succeeded;
                    ViewBag.Message = categoryResponse.Message;
                    ViewBag.Code = categoryResponse.Code;

                    return View();
                }

                Response<CategoryModel> categoryModelResponse = new(categoryResponse.Succeeded);

                foreach (Category category in categoryResponse.Data)
                {
                    category.FillNoteList(ProcessorFactory.CreateNoteProcessor(connString));
                
                    categoryModelResponse.AddItem(new(category));
                }

                return View(categoryModelResponse);
            }
            catch (Exception e)
            {
                ViewBag.Succeeded = false;
                ViewBag.Message = e.Message;
                return View();
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}