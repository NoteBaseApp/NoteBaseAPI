using NoteBaseDALInterface;
using NoteBaseDALInterface.Models;
using NoteBaseLogicInterface;
using NoteBaseLogicInterface.Models;

namespace NoteBaseLogic
{
    public class CategoryProcessor : ICategoryProcessor
    {
        private readonly ICategoryDAL CategoryDAL;
        private readonly INoteProcessor NoteProcessor;
        public CategoryProcessor(ICategoryDAL _categoryDAL, INoteProcessor _noteProcessor)
        {
            CategoryDAL = _categoryDAL;
            NoteProcessor = _noteProcessor;
        }

        public Response<Category> Create(Category _cat)
        {
            Response<Category> response = new(false);

            if (_cat.Title == ""|| _cat.Title == null)
            {
                response.Message = "Title can't be empty";

                return response;
            }

            DALResponse<CategoryDTO> catDALreponse = CategoryDAL.GetByTitle(_cat.Title);
            if (catDALreponse.Data.Count > 0)
            {
                response.Message = "Category With this title already exists";

                return response;
            }

            catDALreponse = CategoryDAL.Create(_cat.ToDTO());

            response = new(catDALreponse.Succeeded)
            {
                Message = catDALreponse.Message,
                Code = catDALreponse.Code
            };

            if (!response.Succeeded)
            {
                return response;
            }

            DALResponse<CategoryDTO> catDALreponseGet = CategoryDAL.GetByTitle(_cat.Title);
            response.AddItem(new(catDALreponseGet.Data[0].ID, catDALreponseGet.Data[0].Title, catDALreponseGet.Data[0].PersonId));

            return response;
        }

        public Response<Category> Delete(int _catId)
        {
            Response<Category> response = new(false);

            Response<Note> noteDALreponse = NoteProcessor.GetByCategory(_catId);
            if (noteDALreponse.Data.Count > 0)
            {
                response.Message = "Notes exist with this category";

                return response;
            }


            DALResponse<CategoryDTO> catDALreponse = CategoryDAL.GetById(_catId);
            if (catDALreponse.Data.Count == 0)
            {
                response.Message = "Category doesn't exist";

                return response;
            }

            catDALreponse = CategoryDAL.Delete(_catId);

            response = new(catDALreponse.Succeeded)
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

            if (catDALreponse.Data.Count == 0)
            {
                return response;
            }

            Category category = new(catDALreponse.Data[0].ID, catDALreponse.Data[0].Title, catDALreponse.Data[0].PersonId);

            response.AddItem(category);

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

        public Response<Category> GetByTitle(string _title)
        {
            DALResponse<CategoryDTO> catDALreponse = CategoryDAL.GetByTitle(_title);

            Response<Category> response = new(catDALreponse.Succeeded)
            {
                Message = catDALreponse.Message,
                Code = catDALreponse.Code
            };

            response.AddItem(new(catDALreponse.Data[0].ID, catDALreponse.Data[0].Title, catDALreponse.Data[0].PersonId));

            return response;
        }

        public Response<Category> Update(Category _cat)
        {
            Response<Category> response = new(false);

            if (_cat.Title == "" || _cat.Title == null)
            {
                response.Message = "Title can't be empty";

                return response;
            }

            DALResponse<CategoryDTO> catDALreponse = CategoryDAL.GetById(_cat.ID);
            if (catDALreponse.Data.Count == 0)
            {
                response.Message = "Category doesn't exist";

                return response;
            }

            catDALreponse = CategoryDAL.Update(_cat.ToDTO());

            response = new(catDALreponse.Succeeded)
            {
                Message = catDALreponse.Message,
                Code = catDALreponse.Code
            };

            if (!response.Succeeded)
            {
                return response;
            }

            DALResponse<CategoryDTO> catDALreponseGet = CategoryDAL.GetById(_cat.ID);
            response.AddItem(new(catDALreponseGet.Data[0].ID, catDALreponseGet.Data[0].Title, catDALreponseGet.Data[0].PersonId));

            return response;
        }

        public Response<Category> Delete(int _catId)
        {
            Response<Category> response = new(false);

            Response<Note> noteDALreponse = NoteProcessor.GetByCategory(_catId);
            if (noteDALreponse.Data.Count > 0)
            {
                response.Message = "Notes exist with this category";

                return response;
            }


            Response<Category> catreponse = GetById(_catId);
            if (catreponse.Data.Count == 0)
            {
                response.Message = "Category doesn't exist";

                return response;
            }

            DALResponse<CategoryDTO> catDALreponse = CategoryDAL.Delete(_catId);

            response = new(catDALreponse.Succeeded)
            {
                Message = catDALreponse.Message,
                Code = catDALreponse.Code
            };

            return response;
        }
    }
}
