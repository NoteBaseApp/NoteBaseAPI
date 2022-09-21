﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using NoteBase.Models;
using NoteBaseInterface;
using NoteBaseLogicFactory;
using System.Diagnostics;

namespace NoteBase.Controllers
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
            string conn = _config.GetConnectionString("NoteBaseConnString");
            INoteProcessor processor = Factory.CreateNoteProcessor(conn);

            return View();
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