using NoteBaseDALInterface;
using NoteBaseDALInterface.Models;

namespace NoteBaseLogicTests.TestDALs
{
    internal class CategoryTestDAL : ICategoryDAL
    {
        public int Create(CategoryDTO _cat)
        {
            return 1;
        }

        public int Delete(int _catId)
        {
            return 1;
        }

        public CategoryDTO GetById(int _catId)
        {
            CategoryDTO categoryDTO = new(_catId, "", 1);

            if (_catId == 999)
            {
                return new(0, "", 0);
            }

            if (_catId == 1)
            {
                categoryDTO = new(1, "Games", 1);
            }

            return categoryDTO;
        }

        public List<CategoryDTO> GetByPerson(int _personId)
        {
            throw new NotImplementedException();
        }

        int GetByTitleCals = 0;

        public CategoryDTO GetByTitle(string _Title)
        {
            if (_Title == "School" && GetByTitleCals > 0)
            {
                return new(12, "School", 1);
            }
            else if (_Title == "NOGames")
            {
                return new(1, "NOGames", 1);
            }

            GetByTitleCals++;

            return new(0, "", 0);
        }

        public int Update(CategoryDTO _cat)
        {
            if (_cat.ID == 999)
            {
                return 0;
            }

            return 1;
        }
    }
}
