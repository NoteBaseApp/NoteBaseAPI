using App.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NoteBaseInterface;
using NoteBaseLogicFactory;
using System.Diagnostics;
using NoteBaseLogicInterface.Models;
using System.Collections.Generic;

namespace App.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _config;
        private readonly string connString;


        public HomeController(ILogger<HomeController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _config = configuration;
            connString = _config.GetConnectionString("NoteBaseConnString");
        }

        [Authorize]
        public IActionResult Index()
        {
            IProcessor<Note> processor = Factory.CreateNoteProcessor(connString);
            Response<Note> response = processor.Get(_UserMail: User.Identity.Name);
            List<Note> notes = (List<Note>)response.Data;
            ViewBag.Data = notes;
            return View();
        }

        [Authorize]
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