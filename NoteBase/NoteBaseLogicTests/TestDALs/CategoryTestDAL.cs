using NoteBaseDALInterface;
using NoteBaseDALInterface.Models;

namespace NoteBaseLogicTests.TestDALs
{
    internal class CategoryTestDAL : ICategoryDAL
    {
        public CategoryDTO Create(string _title, int _personId)
        {
            if (_title == "School")
            {
                return new(12, _title, _personId);
            }
            return new(0, _title, _personId);
        }

        public void Delete(int _catId)
        {
            
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

        public CategoryDTO Update(int _id, string _title, int _personId)
        {
            /* if (_id == 999)
            {
                return new(_id, _title, _personId);
            }*/

            return new(_id, _title, _personId);
        }
    }
}
