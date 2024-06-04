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
    [Route("api/category")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly string connString;
        private readonly IPersonProcessor personProcessor; // for when the DoesPersonExist method gets added
        private readonly ICategoryProcessor categoryProcessor;

        public CategoryController()
        {
            //connString = Environment.GetEnvironmentVariable("DATABASE_URL");
            connString = "Data Source=172.17.0.3,1433;Initial Catalog=NoteBase;User id=NoteBaseAPI;Password=K00kW3kk3r!;Connect Timeout=300;";
            personProcessor = ProcessorFactory.CreatePersonProcessor(connString);
            categoryProcessor = ProcessorFactory.CreateCategoryProcessor(connString);


        }

        // GET: api/<CategoryController>/2
        [HttpGet("GetByPerson/{_personId}")]
        public APIResponse GetByPerson(int _personId)
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
        public APIResponse Get(int _id)
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
            if (_category.PersonId == 0)
            {
                response.Status = APIResponseStatus.Failure;
                response.Message = "No valid PersonId.";
                return response;
            }

            Category category = categoryProcessor.Create(_category.Title, _category.PersonId);

            if (category.ID == 0)
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
        public APIResponse Put(int _id, [FromBody] CategoryRequestParams _category)
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
            if (_category.PersonId == 0)
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
        public APIResponse Delete(int _id)
        {
            APIResponse response = new(APIResponseStatus.Success);

            Category category = categoryProcessor.GetById(_id);

            if (category.ID == 0)
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
