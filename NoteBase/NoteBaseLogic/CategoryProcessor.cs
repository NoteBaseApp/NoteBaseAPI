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

        public bool IsValidTitle(string _title)
        {
            //needs work (entering just spaces should not be seen as valid)
            return _title != "";
        }

        public bool IsTitleUnique(string _title)
        {
            return GetByTitle(_title).ID == Guid.Parse("00000000-0000-0000-0000-000000000000");
        }

        public bool DoesCategoryExits(Guid _id)
        {
            return _id != Guid.Parse("00000000-0000-0000-0000-000000000000") && GetById(_id).ID != Guid.Parse("00000000-0000-0000-0000-000000000000");
        }

        public Category Create(string _title, Guid _personId)
        {
            if (!IsValidTitle(_title))
            {
                throw new ArgumentException("Title can't be empty");
            }

            if (!IsTitleUnique(_title))
            {
                throw new Exception("Category With this title already exists");
            }

            CategoryDTO catDTO = CategoryDAL.Create(_title, _personId);
            return new(catDTO.ID, catDTO.Title, catDTO.PersonId);
        }

        public Category GetById(Guid _catId)
        {
            CategoryDTO catDTO = CategoryDAL.GetById(_catId);

            return new(catDTO.ID, catDTO.Title, catDTO.PersonId);
        }

        public List<Category> GetByPerson(Guid _personId)
        {
            List<Category> categoryList = new();

            List<CategoryDTO> catDTOs = CategoryDAL.GetByPerson(_personId);
            foreach (CategoryDTO catDTO in catDTOs)
            {
                categoryList.Add(new(catDTO.ID, catDTO.Title, catDTO.PersonId));
            }

            return categoryList;
        }

        public Category GetByTitle(string _title)
        {
            CategoryDTO catDTO = CategoryDAL.GetByTitle(_title);

            return new(catDTO.ID, catDTO.Title, catDTO.PersonId);
        }

        public Category Update(Guid _id, string _title, Guid _personId)
        {
            if (!DoesCategoryExits(_id))
            {
                throw new Exception("Category doesn't exist");
            }

            if (!IsValidTitle(_title))
            {
                throw new ArgumentException("Title can't be empty");
            }

            if (!IsTitleUnique(_title))
            {
                throw new Exception("Category With this title already exists");
            }

            CategoryDTO catDTO = CategoryDAL.Update(_id, _title, _personId);
            return new(catDTO.ID, catDTO.Title, catDTO.PersonId);
        }

        public void Delete(Guid _catId)
        {
            List<Note> noteList = NoteProcessor.GetByCategory(_catId);
            if (noteList.Count > 0)
            {
                throw new Exception("Notes exist with this category");
            }


            Category cat = GetById(_catId);
            if (cat.ID == Guid.Parse("00000000-0000-0000-0000-000000000000"))
            {
                throw new Exception("Category doesn't exist");
            }

            CategoryDAL.Delete(_catId);
        }

    }
}
