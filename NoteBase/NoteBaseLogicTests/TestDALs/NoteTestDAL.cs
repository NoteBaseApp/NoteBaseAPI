using NoteBaseDALInterface;
using NoteBaseDALInterface.Models;
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
            throw new NotImplementedException();
        }

        public DALResponse<NoteDTO> CreateNoteTag(int _noteId, int _tagId)
        {
            throw new NotImplementedException();
        }

        public DALResponse<NoteDTO> Delete(int _noteId)
        {
            throw new NotImplementedException();
        }

        public DALResponse<NoteDTO> GetByCategory(int _categoryId)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        public DALResponse<NoteDTO> Update(NoteDTO _note)
        {
            throw new NotImplementedException();
        }
    }
}
