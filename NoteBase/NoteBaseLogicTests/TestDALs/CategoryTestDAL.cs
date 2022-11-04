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
            return _catId switch
            {
                2 => new(false)
                {
                    Code = 3621,
                    Message = ""
                },
                999 => new(false)
                {
                    Code = 1,
                    Message = ""
                },
                _ => new(true),
            };
        }

        public DALResponse<CategoryDTO> GetById(int _catId)
        {
            throw new NotImplementedException();
        }

        public DALResponse<CategoryDTO> GetByPerson(int _personId)
        {
            throw new NotImplementedException();
        }

        int GetByTitleCals = 0;

        public DALResponse<CategoryDTO> GetByTitle(string _Title)
        {
            DALResponse<CategoryDTO> response = new(true);

            if (_Title == "Test" && GetByTitleCals > 0)
            {
                response = new(true);
                response.AddItem(new(12, "Test", 1));

                return response;
            }
            else if (_Title == "TestExist")
            {
                response = new(true);
                response.AddItem(new(1, "TestExist", 1));

                return response;
            }

            GetByTitleCals++;

            return response;
        }

        public DALResponse<CategoryDTO> Update(CategoryDTO _cat)
        {
            throw new NotImplementedException();
        }
    }
}
