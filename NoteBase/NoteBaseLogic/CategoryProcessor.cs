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

        public Category Create(string _title, int _personId)
        {
            if (_title == "")
            {
                throw new ArgumentException("Title can't be empty");
            }

            Category cat = GetByTitle(_title);
            if (cat.ID != 0)
            {
                throw new Exception("Category With this title already exists");
            }

            return new(CategoryDAL.Create(_title, _personId));
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

        public Category Update(int _id, string _title,int _personId)
        {
            Category cat = GetById(_id);
            if (cat.ID == 0)
            {
                throw new Exception("Category doesn't exist");
            }

            if (_title == "")
            {
                throw new ArgumentException("Title can't be empty");
            }

            cat = GetByTitle(_title);
            if (cat.ID != 0)
            {
                throw new Exception("Category With this title already exists");
            }

            return new(CategoryDAL.Update(_id, _title, _personId));
        }

        public void Delete(int _catId)
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

            CategoryDAL.Delete(_catId);
        }

    }
}
