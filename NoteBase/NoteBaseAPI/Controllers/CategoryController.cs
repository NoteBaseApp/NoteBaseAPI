using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NoteBaseAPI.Models;
using NoteBaseLogicFactory;
using NoteBaseLogicInterface;
using NoteBaseLogicInterface.Models;
using System;
using System.Security.Claims;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NoteBaseAPI.Controllers
{
    [Route("category")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly string connString;
        private readonly IPersonProcessor personProcessor; // for when the DoesPersonExist method gets added
        private readonly ICategoryProcessor categoryProcessor;
        private readonly INoteProcessor noteProcessor;

        public CategoryController()
        {
            string? DATA_SOURCE = Environment.GetEnvironmentVariable("DATA_SOURCE");
            string? INITIAL_CATALOG = Environment.GetEnvironmentVariable("INITIAL_CATALOG");
            string? DB_USER_ID = Environment.GetEnvironmentVariable("DB_USER_ID");
            string? DB_PASSWORD = Environment.GetEnvironmentVariable("DB_PASSWORD");
            connString = $"Data Source={DATA_SOURCE};Initial Catalog={INITIAL_CATALOG};User id={DB_USER_ID};Password={DB_PASSWORD};Connect Timeout=300;";
            personProcessor = ProcessorFactory.CreatePersonProcessor(connString);
            categoryProcessor = ProcessorFactory.CreateCategoryProcessor(connString);
            noteProcessor = ProcessorFactory.CreateNoteProcessor(connString);
        }

        [HttpGet("GetByPerson")]
        [Authorize]
        public IActionResult GetByPerson()
        {
            Person? person = GetCurrentUser();

            List<Category> categories = categoryProcessor.GetByPerson(person.ID);

            return Ok(categories);
        }

        [HttpGet("{_id}")]
        [Authorize]
        public IActionResult Get(Guid _id)
        {
            Person? person = GetCurrentUser();

            if (!categoryProcessor.DoesCategoryExits(_id))
            {
                return NotFound(new Error("DoesNotExist", "Category does not exist."));
            }

            Category category = categoryProcessor.GetById(_id);

            if (category.PersonId != person.ID)
            {
                return Forbid();
            }

            category.noteList = noteProcessor.GetByCategory(category.ID);

            return Ok(category);
        }

        [HttpPost]
        [Authorize]
        public IActionResult Post([FromBody] CategoryRequestParams _category)
        {
            Person? person = GetCurrentUser();

            if (!categoryProcessor.IsValidTitle(_category.Title))
            {
                return BadRequest(new Error("NoValidTitle", "Title cannot be empty."));
            }
            if (!categoryProcessor.IsTitleUnique(_category.Title))
            {
                return BadRequest(new Error("AlreadyExists", "Category with this title arleady exists."));
            }

            Category category = categoryProcessor.Create(_category.Title, person.ID);

            if (category.ID == Guid.Parse("00000000-0000-0000-0000-000000000000"))
            {
                throw new Exception("Category could not be created.");
            }

            return Ok(category);
        }

        [HttpPut]
        [Authorize]
        public IActionResult Put([FromBody] CategoryRequestParams _category)
        {
            Person? person = GetCurrentUser();

            if (!categoryProcessor.DoesCategoryExits(_category.ID))
            {
                return NotFound(new Error("DoesNotExist", "Category does not exist."));
            }

            Category category = categoryProcessor.GetById(_category.ID);

            if (category.PersonId != person.ID)
            {
                return Forbid();
            }

            if (!categoryProcessor.IsValidTitle(_category.Title))
            {
                return BadRequest(new Error("NoValidTitle", "Title cannot be empty."));
            }
            if (!categoryProcessor.IsTitleUnique(_category.Title))
            {
                return BadRequest(new Error("AlreadyExists", "Category with this title arleady exists."));
            }

            category = categoryProcessor.Update(_category.ID, _category.Title, person.ID);

            return Ok(category);
        }

        [HttpDelete("{_id}")]
        [Authorize]
        public IActionResult Delete(Guid _id)
        {
            Person? person = GetCurrentUser();

            Category category = categoryProcessor.GetById(_id);

            if (category.ID == Guid.Parse("00000000-0000-0000-0000-000000000000"))
            {
                return NotFound(new Error("DoesNotExist", "Category does not exist."));
            }

            if (category.PersonId != person.ID)
            {
                return Forbid();
            }

            categoryProcessor.Delete(category.ID);

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
