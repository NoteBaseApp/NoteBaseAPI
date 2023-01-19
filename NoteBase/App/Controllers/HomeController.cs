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
            this.person = personProcessor.GetByEmail(User.Identity.Name);

            ICategoryProcessor categoryProcessor = ProcessorFactory.CreateCategoryProcessor(connString);
            List<Category> categories = categoryProcessor.GetByPerson(person.ID);
            List<CategoryModel> categoryModels = new();

            foreach (Category category in categories)
            {
                category.FillNoteList(ProcessorFactory.CreateNoteProcessor(connString));
                
                categoryModels.Add(new(category));
            }

            return View(categoryModels);
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