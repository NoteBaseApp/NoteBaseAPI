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
            Response<Person> personResponse = personProcessor.GetByEmail(User.Identity.Name);
            person = personResponse.Data[0];

            ICategoryProcessor categoryProcessor = ProcessorFactory.CreateCategoryProcessor(connString);
            Response<Category> categoryResponse = categoryProcessor.GetByPerson(person.ID);

            Response<CategoryModel> categoryModelResponse = new(categoryResponse.Succeeded) 
            { 
                Code = categoryResponse.Code,
                Message = categoryResponse.Message
            };

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

                categoryModelResponse.AddItem(categoryModel);
            }


            return View(categoryModelResponse);
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