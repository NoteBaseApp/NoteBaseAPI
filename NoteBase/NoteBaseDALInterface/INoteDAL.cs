using NoteBaseDALInterface.Models;

namespace NoteBaseDALInterface
{
    public interface INoteDAL
    {
        public DALResponse<NoteDTO> Create(NoteDTO _note);
        public DALResponse<NoteDTO> CreateNoteTag(int _noteId, int _tagId);
        public DALResponse<NoteDTO> Get(int _noteId);
        public DALResponse<NoteDTO> Get(string _userId);
        public DALResponse<NoteDTO> GetByTitle(string _Title);
        public DALResponse<NoteDTO> Update(int _noteId, NoteDTO _note);
        public DALResponse<NoteDTO> Delete(int _noteId);
    }
}
