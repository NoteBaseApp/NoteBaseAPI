using NoteBaseDALInterface;
using NoteBaseDALInterface.Models;
using NoteBaseLogicInterface;
using NoteBaseLogicInterface.Models;

namespace NoteBaseLogic
{
    public class CategoryProcessor : ICategoryProcessor
    {
        private readonly ICategoryDAL CategoryDAL;
        public CategoryProcessor(ICategoryDAL _categoryDAL)
        {
            CategoryDAL = _categoryDAL;
        }

        public Response<Category> Create(Category _cat)
        {
            Response<Category> response = new(false);

            if (_cat.Title == "")
            {
                response.Message = "Title cant be empty";

                return response;
            }

            DALResponse<CategoryDTO> catDALreponse = CategoryDAL.Create(_cat.ToDTO());

            response = new(catDALreponse.Succeeded)
            {
                Message = catDALreponse.Message,
                Code = catDALreponse.Code
            };

            return response;
        }

        public Response<Category> Delete(int _catId)
        {
            DALResponse<CategoryDTO> catDALreponse = CategoryDAL.Delete(_catId);

            Response<Category> response = new(catDALreponse.Succeeded)
            {
                Message = catDALreponse.Message,
                Code = catDALreponse.Code
            };

            return response;
        }

        public Response<Category> GetById(int _catId)
        {
            DALResponse<CategoryDTO> catDALreponse = CategoryDAL.GetById(_catId);

            Response<Category> response = new(catDALreponse.Succeeded)
            {
                Message = catDALreponse.Message,
                Code = catDALreponse.Code
            };

            response.AddItem(new(catDALreponse.Data[0].ID, catDALreponse.Data[0].Title, catDALreponse.Data[0].PersonId));

            return response;
        }

        public Response<Category> GetByPerson(int _personId)
        {
            DALResponse<CategoryDTO> catDALreponse = CategoryDAL.GetByPerson(_personId);

            Response<Category> response = new(catDALreponse.Succeeded)
            {
                Message = catDALreponse.Message,
                Code = catDALreponse.Code
            };

            foreach (CategoryDTO item in catDALreponse.Data)
            {
                response.AddItem(new(item.ID, item.Title, item.PersonId));
            }

            return response;
        }

        /* public Response<Category> GetByTitle(string _title)
        {
            DALResponse<CategoryDTO> catDALreponse = CategoryDAL.GetById(_catId);

            Response<Category> response = new(catDALreponse.Status, catDALreponse.Message);

            response.AddItem(new(catDALreponse.Data[0].ID, catDALreponse.Data[0].Title, catDALreponse.Data[0].PersonId));

            return response;
        } */

        public Response<Category> Update(Category _cat)
        {
            DALResponse<CategoryDTO> catDALreponse = CategoryDAL.Update(_cat.ToDTO());

            Response<Category> response = new(catDALreponse.Succeeded)
            {
                Message = catDALreponse.Message,
                Code = catDALreponse.Code
            };

            return response;
        }
    }
}
