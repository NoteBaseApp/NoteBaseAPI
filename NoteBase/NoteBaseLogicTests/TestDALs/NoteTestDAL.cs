using NoteBaseDALInterface;
using NoteBaseDALInterface.Models;
using NoteBaseLogicInterface.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteBaseLogicTests.TestDALs
{
    internal class NoteTestDAL : INoteDAL
    {
        public NoteDTO Create(string _title, string _text, Guid _categoryId, Guid _personId)
        {
            NoteDTO result = new(Guid.Parse("00000000-0000-0000-0000-000000000000"), _title, _text, _categoryId, _personId);
            return result;
        }

        public void Delete(Guid _noteId)
        {
           
        }

        public List<NoteDTO> GetByCategory(Guid _categoryId)
        {
            List<NoteDTO> result = new();

            if (_categoryId == Guid.Parse("3fceb4e1-6fa5-41c0-9fbf-77cec3b7aec9"))
            {
                return result;
            }

            if (_categoryId != Guid.Parse("2344dd8f-8a22-4fe2-bfd9-74bde5d42395"))
            {
                result.Add(new(Guid.Parse("00000000-0000-0000-0000-000000000000"), "", "", Guid.Parse("00000000-0000-0000-0000-000000000000"), Guid.Parse("00000000-0000-0000-0000-000000000000")));
                return result;
            }

            result.Add(new(Guid.Parse("59d242ee-f05f-4ce1-a9ff-7c52d993ff58"), "", "", _categoryId, Guid.Parse("4e8d41a5-790a-4a11-b6c2-b4d37b6fd38f")));

            return result;
        }

        public NoteDTO GetById(Guid _noteId)
        {
            throw new NotImplementedException();
        }

        public List<NoteDTO> GetByPerson(Guid _personId)
        {
            throw new NotImplementedException();
        }

        public List<NoteDTO> GetByTag(Guid _tagId)
        {
            throw new NotImplementedException();
        }

        int GetByTitleCals = 0;

        public NoteDTO GetByTitle(string _Title)
        {

            if (_Title == "School" && GetByTitleCals > 0)
            {
               NoteDTO note = new(Guid.Parse("555660fe-82c2-42ac-88e7-887102331de3"), "School", "Ik zit op #Fontys in #Eindhoven", Guid.Parse("3fceb4e1-6fa5-41c0-9fbf-77cec3b7aec9"), Guid.Parse("4e8d41a5-790a-4a11-b6c2-b4d37b6fd38f"));
                note.tagList.Add(new(Guid.Parse("df47ed24-14b9-42da-b2c5-0966e31eca84"), "fontys"));
                note.tagList.Add(new(Guid.Parse("82c14bf7-53a4-4587-8eab-0aa59b0a48c9"), "eindhoven"));

                return note;
            }
            else if (_Title == "Gaming" && GetByTitleCals > 0)
            {
                return new(Guid.Parse("f8486f1a-6fd4-47ed-b4b0-3804f7fadce1"), "Gaming", "Ik ga zaterdag gamen", Guid.Parse("3fceb4e1-6fa5-41c0-9fbf-77cec3b7aec9"), Guid.Parse("4e8d41a5-790a-4a11-b6c2-b4d37b6fd38f"));
            }

            GetByTitleCals++;

            return new(Guid.Parse("00000000-0000-0000-0000-000000000000"), "", "", Guid.Parse("00000000-0000-0000-0000-000000000000"), Guid.Parse("00000000-0000-0000-0000-000000000000"));
        }

        public NoteDTO Update(Guid id, string _title, string _text, Guid _categoryId)
        {
            throw new NotImplementedException();
        }
    }
}
