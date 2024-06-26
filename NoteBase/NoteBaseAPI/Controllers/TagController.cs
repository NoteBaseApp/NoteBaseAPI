﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NoteBaseAPI.Models;
using NoteBaseLogicFactory;
using NoteBaseLogicInterface;
using NoteBaseLogicInterface.Models;
using System.Security.Claims;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NoteBaseAPI.Controllers
{
    [Route("tag")]
    [ApiController]
    public class TagController : ControllerBase
    {
        private readonly string connString;
        private readonly ITagProcessor tagProcessor;
        private readonly IPersonProcessor personProcessor;

        public TagController()
        {
            string? DATA_SOURCE = Environment.GetEnvironmentVariable("DATA_SOURCE");
            string? INITIAL_CATALOG = Environment.GetEnvironmentVariable("INITIAL_CATALOG");
            string? DB_USER_ID = Environment.GetEnvironmentVariable("DB_USER_ID");
            string? DB_PASSWORD = Environment.GetEnvironmentVariable("DB_PASSWORD");
            connString = $"Data Source={DATA_SOURCE};Initial Catalog={INITIAL_CATALOG};User id={DB_USER_ID};Password={DB_PASSWORD};Connect Timeout=300;";
            tagProcessor = ProcessorFactory.CreateTagProcessor(connString);
            personProcessor = ProcessorFactory.CreatePersonProcessor(connString);
        }

        [HttpGet("GetByPerson")]
        [Authorize]
        public IActionResult GetByPerson()
        {
            Person? person = GetCurrentUser();

            List<Tag> tag = tagProcessor.GetByPerson(person.ID);

            return Ok(tag);
        }

        [HttpGet("GetByTitle/{_Title}")]
        [Authorize]
        public IActionResult GetByTitle(string _Title)
        {
            Tag tag = tagProcessor.GetByTitle(_Title);

            if (tag.ID == Guid.Parse("00000000-0000-0000-0000-000000000000"))
            {
                return NotFound(new Error("DoesNotExist", "Tag does not exist."));
            }

            return Ok(tag);
        }

        [HttpGet("{_id}")]
        [Authorize]
        public IActionResult Get(Guid _id)
        {
            Tag tag = tagProcessor.GetById(_id);

            if (tag.ID == Guid.Parse("00000000-0000-0000-0000-000000000000"))
            {
                return NotFound(new Error("DoesNotExist", "Tag does not exist."));
            }

            return Ok();
        }

        private Person? GetCurrentUser()
        {
            ClaimsIdentity? identity = HttpContext.User.Identity as ClaimsIdentity;

            if (identity == null || identity.Claims.Count() <= 0)
            {
                return null;
            }
            IEnumerable<Claim> userClaims = identity.Claims;

            return personProcessor.GetByEmail(userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Email)?.Value);
        }
    }
}
