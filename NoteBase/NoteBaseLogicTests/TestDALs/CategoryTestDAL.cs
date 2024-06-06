using NoteBaseDALInterface;
using NoteBaseDALInterface.Models;

namespace NoteBaseLogicTests.TestDALs
{
    internal class CategoryTestDAL : ICategoryDAL
    {
        public CategoryDTO Create(string _title, Guid _personId)
        {
            if (_title == "School")
            {
                return new(Guid.Parse("de55078e-2426-4a1f-b6bf-d3b288022eda"), _title, _personId);
            }
            return new(Guid.Parse("00000000-0000-0000-0000-000000000000"), _title, _personId);
        }

        public void Delete(Guid _catId)
        {
            
        }

        public CategoryDTO GetById(Guid _catId)
        {
            CategoryDTO categoryDTO = new(_catId, "", Guid.Parse("4e8d41a5-790a-4a11-b6c2-b4d37b6fd38f"));

            if (_catId == Guid.Parse("12345678-1234-1234-1234-123456789123"))
            {
                return new(Guid.Parse("00000000-0000-0000-0000-000000000000"), "", Guid.Parse("00000000-0000-0000-0000-000000000000"));
            }

            if (_catId == Guid.Parse("f2a2f10b-aafd-43c2-b848-12421f1fa88f"))
            {
                categoryDTO = new(Guid.Parse("f2a2f10b-aafd-43c2-b848-12421f1fa88f"), "Games", Guid.Parse("4e8d41a5-790a-4a11-b6c2-b4d37b6fd38f"));
            }

            return categoryDTO;
        }

        public List<CategoryDTO> GetByPerson(Guid _personId)
        {
            throw new NotImplementedException();
        }

        int GetByTitleCals = 0;

        public CategoryDTO GetByTitle(string _Title)
        {
            if (_Title == "School" && GetByTitleCals > 0)
            {
                return new(Guid.Parse("d8bc35c0-5fe4-484c-abfd-3a74a1c90ad7"), "School", Guid.Parse("4e8d41a5-790a-4a11-b6c2-b4d37b6fd38f"));
            }
            else if (_Title == "NOGames")
            {
                return new(Guid.Parse("f2a2f10b-aafd-43c2-b848-12421f1fa88f"), "NOGames", Guid.Parse("4e8d41a5-790a-4a11-b6c2-b4d37b6fd38f"));
            }

            GetByTitleCals++;

            return new(Guid.Parse("00000000-0000-0000-0000-000000000000"), "", Guid.Parse("00000000-0000-0000-0000-000000000000"));
        }

        public CategoryDTO Update(Guid _id, string _title, Guid _personId)
        {
            /* if (_id == 999)
            {
                return new(_id, _title, _personId);
            }*/

            return new(_id, _title, _personId);
        }
    }
}
