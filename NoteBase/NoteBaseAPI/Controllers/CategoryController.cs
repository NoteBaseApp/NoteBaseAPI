using Microsoft.AspNetCore.Authorization;
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
    [Authorize]
    [Route("category")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly string connString;
        private readonly IPersonProcessor personProcessor; // for when the DoesPersonExist method gets added
        private readonly ICategoryProcessor categoryProcessor;

        public CategoryController()
        {
            string DATA_SOURCE = Environment.GetEnvironmentVariable("DATA_SOURCE");
            string INITIAL_CATALOG = Environment.GetEnvironmentVariable("INITIAL_CATALOG");
            string DB_USER_ID = Environment.GetEnvironmentVariable("DB_USER_ID");
            string DB_PASSWORD = Environment.GetEnvironmentVariable("DB_PASSWORD");
            connString = $"Data Source={DATA_SOURCE};Initial Catalog={INITIAL_CATALOG};User id={DB_USER_ID};Password={DB_PASSWORD};Connect Timeout=300;";
            personProcessor = ProcessorFactory.CreatePersonProcessor(connString);
            categoryProcessor = ProcessorFactory.CreateCategoryProcessor(connString);


        }

        // GET: api/<CategoryController>/2
        [HttpGet("GetByPerson/{_personId}")]
        public APIResponse GetByPerson(Guid _personId)
        {
            APIResponse response = new(APIResponseStatus.Success);

            List<Category> categories = categoryProcessor.GetByPerson(_personId);

            if (categories == null || categories.Count == 0)
            {
                response.Status = APIResponseStatus.Failure;
                response.Message = "No categories where found.";
                return response;
            }

            response.ResponseBody = categories;
            return response;
        }

        // GET api/<CategoryController>/5
        [HttpGet("{_id}")]
        public APIResponse Get(Guid _id)
        {
            APIResponse response = new(APIResponseStatus.Success);

            if (!categoryProcessor.DoesCategoryExits(_id))
            {
                response.Status = APIResponseStatus.Failure;
                response.Message = "Category does not exist.";
                return response;
            }

            Category category = categoryProcessor.GetById(_id);

            response.ResponseBody = category;
            return response;
        }

        // POST api/<CategoryController>
        [HttpPost]
        public APIResponse Post([FromBody] CategoryRequestParams _category)
        {
            APIResponse response = new(APIResponseStatus.Success);

            if (!categoryProcessor.IsValidTitle(_category.Title))
            {
                response.Status = APIResponseStatus.Failure;
                response.Message = "Title cannot be empty.";
                return response;
            }
            if (!categoryProcessor.IsTitleUnique(_category.Title))
            {
                response.Status = APIResponseStatus.Failure;
                response.Message = "Category with this title arleady exists.";
                return response;
            }
            if (_category.PersonId == Guid.Parse("00000000-0000-0000-0000-000000000000"))
            {
                response.Status = APIResponseStatus.Failure;
                response.Message = "No valid PersonId.";
                return response;
            }

            Category category = categoryProcessor.Create(_category.Title, _category.PersonId);

            if (category.ID == Guid.Parse("00000000-0000-0000-0000-000000000000"))
            {
                response.Status = APIResponseStatus.Failure;
                response.Message = "Category could not be created.";
                return response;
            }

            response.ResponseBody = category;
            return response;
        }

        // PUT api/<CategoryController>/5
        [HttpPut("{_id}")]
        public APIResponse Put(Guid _id, [FromBody] CategoryRequestParams _category)
        {
            APIResponse response = new(APIResponseStatus.Success);


            if (!categoryProcessor.DoesCategoryExits(_id))
            {
                response.Status = APIResponseStatus.Failure;
                response.Message = "Category does not exist.";
                return response;
            }
            if (!categoryProcessor.IsValidTitle(_category.Title))
            {
                response.Status = APIResponseStatus.Failure;
                response.Message = "Title cannot be empty.";
                return response;
            }
            if (!categoryProcessor.IsTitleUnique(_category.Title))
            {
                response.Status = APIResponseStatus.Failure;
                response.Message = "Category with this title arleady exists.";
                return response;
            }
            if (_category.PersonId == Guid.Parse("00000000-0000-0000-0000-000000000000"))
            {
                response.Status = APIResponseStatus.Failure;
                response.Message = "No valid PersonId.";
                return response;
            }

            Category category = categoryProcessor.GetById(_id);
            category = categoryProcessor.Update(_id, _category.Title, _category.PersonId);

            response.ResponseBody = category;
            return response;
        }

        // DELETE api/<CategoryController>/5
        [HttpDelete("{_id}")]
        public APIResponse Delete(Guid _id)
        {
            APIResponse response = new(APIResponseStatus.Success);

            Category category = categoryProcessor.GetById(_id);

            if (category.ID == Guid.Parse("00000000-0000-0000-0000-000000000000"))
            {
                response.Status = APIResponseStatus.Failure;
                response.Message = "Category does not exist.";
                return response;
            }

            categoryProcessor.Delete(category.ID);

            return response;
        }
    }
}
