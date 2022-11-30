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
        public DALResponse<NoteDTO> Create(NoteDTO _note)
        {
            return new(true);
        }

        public DALResponse<NoteDTO> CreateNoteTag(int _noteId, int _tagId)
        {
            return new(true);
        }

        public DALResponse<NoteDTO> Delete(int _noteId)
        {
            throw new NotImplementedException();
        }

        public DALResponse<NoteDTO> DeleteNoteTag(int _noteId)
        {
            throw new NotImplementedException();
        }

        public DALResponse<NoteDTO> GetByCategory(int _categoryId)
        {
            DALResponse<NoteDTO> dALResponse = new(true);
            if (_categoryId != 2)
            {
                return dALResponse;
            }

            NoteDTO noteDTO = new(1, "", "", _categoryId);
            dALResponse.AddItem(noteDTO);

            return dALResponse;
        }

        public DALResponse<NoteDTO> GetById(int _noteId)
        {
            throw new NotImplementedException();
        }

        public DALResponse<NoteDTO> GetByPerson(int _personId)
        {
            throw new NotImplementedException();
        }

        public DALResponse<NoteDTO> GetByTitle(string _Title)
        {
            DALResponse<NoteDTO> response = new(true);

            if (_Title == "School")
            {
               NoteDTO note = new(20, "School", "Ik zit op #Fontys in #Eindhoven", 1);
                note.TryAddTagDTO(new(11, "fontys"));
                note.TryAddTagDTO(new(12, "eindhoven"));
                response.AddItem(note);
            }
            else if (_Title == "Gaming")
            {
                response.AddItem(new(21, "Gaming", "Ik ga zaterdag gamen", 1));
            }

            return response;
        }

        public DALResponse<NoteDTO> Update(NoteDTO _note)
        {
            throw new NotImplementedException();
        }
    }
}
