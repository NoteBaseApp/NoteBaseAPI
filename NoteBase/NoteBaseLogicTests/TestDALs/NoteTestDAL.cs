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
        public NoteDTO Create(string _title, string _text, int _categoryId, int _personId)
        {
            NoteDTO result = new(0, _title, _text, _categoryId, _personId);
            return result;
        }

        public void Delete(int _noteId)
        {
           
        }

        public List<NoteDTO> GetByCategory(int _categoryId)
        {
            List<NoteDTO> result = new();

            if (_categoryId == 1)
            {
                return result;
            }

            if (_categoryId != 2)
            {
                result.Add(new(0, "", "", 0, 0));
                return result;
            }

            result.Add(new(1, "", "", _categoryId, 1));

            return result;
        }

        public NoteDTO GetById(int _noteId)
        {
            throw new NotImplementedException();
        }

        public List<NoteDTO> GetByPerson(int _personId)
        {
            throw new NotImplementedException();
        }

        public List<NoteDTO> GetByTag(int _tagId)
        {
            throw new NotImplementedException();
        }

        int GetByTitleCals = 0;

        public NoteDTO GetByTitle(string _Title)
        {

            if (_Title == "School" && GetByTitleCals > 0)
            {
               NoteDTO note = new(20, "School", "Ik zit op #Fontys in #Eindhoven", 1, 1);
                note.tagList.Add(new(11, "fontys"));
                note.tagList.Add(new(12, "eindhoven"));

                return note;
            }
            else if (_Title == "Gaming" && GetByTitleCals > 0)
            {
                return new(21, "Gaming", "Ik ga zaterdag gamen", 1, 1);
            }

            GetByTitleCals++;

            return new(0, "", "", 0, 0);
        }

        public NoteDTO Update(int id, string _title, string _text, int _categoryId)
        {
            throw new NotImplementedException();
        }
    }
}
