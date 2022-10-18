using NoteBaseDALInterface.Models;

namespace NoteBaseDALInterface
{
    public interface INoteDAL
    {
        public DALResponse<NoteDTO> Create(NoteDTO _note);
        public DALResponse<NoteDTO> CreateNoteTag(int _noteId, int _tagId);
        public DALResponse<NoteDTO> GetById(int _noteId);
        public DALResponse<NoteDTO> GetByPerson(int _personId);
        public DALResponse<NoteDTO> GetByTitle(string _Title);
        public DALResponse<NoteDTO> Update(NoteDTO _note);
        public DALResponse<NoteDTO> Delete(int _noteId);
    }
}
