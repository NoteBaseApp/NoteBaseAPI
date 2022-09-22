using App.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NoteBaseInterface;
using NoteBaseLogicFactory;
using System.Diagnostics;
using NoteBaseLogicInterface.Models;

namespace App.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _config;


        public HomeController(ILogger<HomeController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _config = configuration;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public IActionResult Privacy()
        {
            IProcessor<Tag> processor = Factory.CreateTagProcessor(_config.GetConnectionString("NoteBaseConnString"));
            //IProcessor<Tag> processor = Factory.CreateTagProcessor("Data Source=LAPTOP-AK9JEN2V;Initial Catalog=NoteBase;Integrated Security=True;Connect Timeout=300;");
            Response<Tag> response = processor.Get(_UserMail: User.Identity.Name);

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}