using NoteBaseDALInterface.Models;

namespace NoteBaseDALInterface
{
    public interface INoteDAL
    {
        public DALResponse<NoteDTO> CreateNote(NoteDTO _note);
        public DALResponse<NoteDTO> GetNote(int _noteId);
        public DALResponse<NoteDTO> GetNote();
        public DALResponse<NoteDTO> UpdateNote(int _noteId, NoteDTO _note);
        public DALResponse<NoteDTO> DeleteNote(int _noteId);
    }
}