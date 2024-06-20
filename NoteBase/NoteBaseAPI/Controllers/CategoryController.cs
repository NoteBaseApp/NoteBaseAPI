using Microsoft.AspNetCore.Mvc;
using NoteBaseAPI.Models;
using NoteBaseLogicFactory;
using NoteBaseLogicInterface;
using NoteBaseLogicInterface.Models;
using System.Security.Cryptography;
using System.Xml.Linq;

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

        public CategoryController()
        {
            string? DATA_SOURCE = Environment.GetEnvironmentVariable("DATA_SOURCE");
            string? INITIAL_CATALOG = Environment.GetEnvironmentVariable("INITIAL_CATALOG");
            string? DB_USER_ID = Environment.GetEnvironmentVariable("DB_USER_ID");
            string? DB_PASSWORD = Environment.GetEnvironmentVariable("DB_PASSWORD");
            connString = $"Data Source={DATA_SOURCE};Initial Catalog={INITIAL_CATALOG};User id={DB_USER_ID};Password={DB_PASSWORD};Connect Timeout=300;";
            personProcessor = ProcessorFactory.CreatePersonProcessor(connString);
            categoryProcessor = ProcessorFactory.CreateCategoryProcessor(connString);


        }

        // GET: api/<CategoryController>/2
        [HttpGet("GetByPerson/{_personId}")]
        public IActionResult GetByPerson(Guid _personId)
        {
            List<Category> categories = categoryProcessor.GetByPerson(_personId);

            return Ok(categories);
        }

        // GET api/<CategoryController>/5
        [HttpGet("{_id}")]
        public IActionResult Get(Guid _id)
        {
            if (!categoryProcessor.DoesCategoryExits(_id))
            {
                return NotFound(new Error("DoesNotExist", "Category does not exist."));
            }

            Category category = categoryProcessor.GetById(_id);

            return Ok(category);
        }

        // POST api/<CategoryController>
        [HttpPost]
        public IActionResult Post([FromBody] CategoryRequestParams _category)
        {
            if (!categoryProcessor.IsValidTitle(_category.Title))
            {
                return BadRequest(new Error("InValidTitle", "Title cannot be empty."));
            }
            if (!categoryProcessor.IsTitleUnique(_category.Title))
            {
                return BadRequest(new Error("AlreadyExists", "Category with this title arleady exists."));
            }
            if (_category.PersonId == Guid.Parse("00000000-0000-0000-0000-000000000000"))
            {
                return BadRequest(new Error("NoValidId", "No valid PersonId."));
            }

            Category category = categoryProcessor.Create(_category.Title, _category.PersonId);

            if (category.ID == Guid.Parse("00000000-0000-0000-0000-000000000000"))
            {
                throw new Exception("Category could not be created.");
            }

            return Ok(category);
        }

        // PUT api/<CategoryController>/5
        [HttpPut("{_id}")]
        public IActionResult Put(Guid _id, [FromBody] CategoryRequestParams _category)
        {
            if (!categoryProcessor.DoesCategoryExits(_id))
            {
                return NotFound(new Error("DoesNotExist", "Category does not exist."));
            }
            if (!categoryProcessor.IsValidTitle(_category.Title))
            {
                return BadRequest(new Error("InValidTitle", "Title cannot be empty."));
            }
            if (!categoryProcessor.IsTitleUnique(_category.Title))
            {
                return BadRequest(new Error("AlreadyExists", "Category with this title arleady exists."));
            }
            if (_category.PersonId == Guid.Parse("00000000-0000-0000-0000-000000000000"))
            {
                return BadRequest(new Error("NoValidId", "No valid PersonId."));
            }

            Category category = categoryProcessor.GetById(_id);
            category = categoryProcessor.Update(_id, _category.Title, _category.PersonId);

            return Ok(category);
        }

        // DELETE api/<CategoryController>/5
        [HttpDelete("{_id}")]
        public IActionResult Delete(Guid _id)
        {
            Category category = categoryProcessor.GetById(_id);

            if (category.ID == Guid.Parse("00000000-0000-0000-0000-000000000000"))
            {
                return NotFound(new Error("DoesNotExist", "Category does not exist."));
            }

            categoryProcessor.Delete(category.ID);

            return Ok();
        }
    }
}
