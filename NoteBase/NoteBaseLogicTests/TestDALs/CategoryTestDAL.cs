using NoteBaseDALInterface;
using NoteBaseDALInterface.Models;

namespace NoteBaseLogicTests.TestDALs
{
    internal class CategoryTestDAL : ICategoryDAL
    {
        public DALResponse<CategoryDTO> Create(CategoryDTO _cat)
        {
            return new(true);
        }

        public DALResponse<CategoryDTO> Delete(int _catId)
        {
            return new(true);
        }

        public DALResponse<CategoryDTO> GetById(int _catId)
        {
            DALResponse<CategoryDTO> response = new(true);

            if (_catId == 999)
            {
                return new(true);
            }

            CategoryDTO categoryDTO = new(_catId, "", 1);
            response.AddItem(categoryDTO);
            return response;
        }

        public DALResponse<CategoryDTO> GetByPerson(int _personId)
        {
            throw new NotImplementedException();
        }

        int GetByTitleCals = 0;

        public DALResponse<CategoryDTO> GetByTitle(string _Title)
        {
            DALResponse<CategoryDTO> response = new(true);

            if (_Title == "School" && GetByTitleCals > 0)
            {
                response = new(true);
                response.AddItem(new(12, "School", 1));

                return response;
            }
            else if (_Title == "Games")
            {
                response = new(true);
                response.AddItem(new(1, "Games", 1));

                return response;
            }

            GetByTitleCals++;

            return response;
        }

        public DALResponse<CategoryDTO> Update(CategoryDTO _cat)
        {
            DALResponse<CategoryDTO> response = new(true);

            if (_cat.ID == 999)
            {
                return new(false);
            }

            return response;
        }
    }
}
