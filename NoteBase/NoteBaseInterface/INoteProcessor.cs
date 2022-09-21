using NoteBaseLogicInterface.Models;

namespace NoteBaseInterface
{
    public interface INoteProcessor
    {
        public Response<Note> CreateNote(Note _note);
        public Response<Note> GetNote(int _noteId);
        public Response<Note> GetNote();
        public Response<Note> UpdateNote(int _noteId, Note _note);
        public Response<Note> DeleteNote(int _noteId);
    }
}