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

        public Category Create(Category _cat)
        {
            if (_cat.Title == "")
            {
                throw new ArgumentException("Title can't be empty");
            }

            Category cat = GetByTitle(_cat.Title);
            if (cat.ID != 0)
            {
                throw new Exception("Category With this title already exists");
            }

            CategoryDAL.Create(_cat.ToDTO());

            return GetByTitle(_cat.Title);
        }

        public Category GetById(int _catId)
        {
            CategoryDTO catDTO = CategoryDAL.GetById(_catId);

            return new(catDTO);
        }

        public List<Category> GetByPerson(int _personId)
        {
            List<Category> result = new();

            List<CategoryDTO> catDTOs = CategoryDAL.GetByPerson(_personId);
            foreach (CategoryDTO categoryDTO in catDTOs)
            {
                result.Add(new(categoryDTO));
            }

            return result;
        }

        public Category GetByTitle(string _title)
        {
            CategoryDTO catDTO = CategoryDAL.GetByTitle(_title);

            return new(catDTO);
        }

        public Category Update(Category _cat)
        {
            if (_cat.Title == "")
            {
                throw new ArgumentException("Title can't be empty");
            }

            Category cat = GetByTitle(_cat.Title);
            if (cat.ID != 0)
            {
                throw new Exception("Category With this title already exists");
            }

            CategoryDAL.Update(_cat.ToDTO());

            return GetById(_cat.ID);
        }

        public int Delete(int _catId)
        {
            List<Note> notes = NoteProcessor.GetByCategory(_catId);
            if (notes.Count > 0)
            {
                throw new Exception("Notes exist with this category");
            }


            Category cat = GetById(_catId);
            if (cat.ID == 0)
            {
                throw new Exception("Category doesn't exist");
            }

            return CategoryDAL.Delete(_catId);
        }

    }
}
