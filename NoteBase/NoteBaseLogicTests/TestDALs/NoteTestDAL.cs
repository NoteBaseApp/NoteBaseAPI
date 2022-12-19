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
        public int Create(NoteDTO _note)
        {
            return 1;
        }

        public int CreateNoteTag(int _noteId, int _tagId)
        {
            return 1;
        }

        public int Delete(int _noteId)
        {
            throw new NotImplementedException();
        }

        public int DeleteNoteTag(int _noteId)
        {
            throw new NotImplementedException();
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
                result.Add(new(0, "", "", 0));
                return result;
            }

            result.Add(new(1, "", "", _categoryId));

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
               NoteDTO note = new(20, "School", "Ik zit op #Fontys in #Eindhoven", 1);
                note.TryAddTagDTO(new(11, "fontys"));
                note.TryAddTagDTO(new(12, "eindhoven"));

                return note;
            }
            else if (_Title == "Gaming")
            {
                return new(21, "Gaming", "Ik ga zaterdag gamen", 1);
            }

            GetByTitleCals++;

            return new(0, "", "", 0);
        }

        public int Update(NoteDTO _note)
        {
            throw new NotImplementedException();
        }
    }
}
