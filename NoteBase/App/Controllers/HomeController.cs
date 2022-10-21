using App.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NoteBaseDALFactory;
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

            personProcessor = ProcessorFactory.CreatePersonProcessor(DALFactory.CreatePersonDAL(connString));
        }

        [Authorize]
        public IActionResult Index()
        {
            Response<Person> personResponse = personProcessor.GetByEmail(User.Identity.Name);
            person = personResponse.Data[0];

            INoteProcessor noteProcessor = ProcessorFactory.CreateNoteProcessor(DALFactory.CreateNoteDAL(connString), DALFactory.CreateTagDAL(connString), DALFactory.CreateCategoryDAL(connString));
            Response<Note> noteResponse = noteProcessor.GetByPerson(person.ID);

            ICategoryProcessor categoryProcessor = ProcessorFactory.CreateCategoryProcessor(DALFactory.CreateCategoryDAL(connString));
            Response<Category> categoryResponse = categoryProcessor.GetByPerson(person.ID);

            dynamic data = new ExpandoObject();
            data.noteResponse = noteResponse;
            data.categoryResponse = categoryResponse;

            return View(data);
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