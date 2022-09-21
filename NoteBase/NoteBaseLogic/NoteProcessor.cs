using NoteBaseDALFactory;
using NoteBaseDALInterface;
using NoteBaseInterface;
using NoteBaseLogicInterface.Models;

namespace NoteBaseLogic
{
    public class NoteProcessor : INoteProcessor
    {
        private readonly INoteDAL NoteDAL;
        public NoteProcessor(string _connString)
        {
            NoteDAL = Factory.CreateNoteDAL(_connString);
        }

        public Response<Note> CreateNote(Note _note)
        {
            throw new NotImplementedException();
        }

        public Response<Note> GetNote(int _noteId)
        {
            throw new NotImplementedException();
        }

        public Response<Note> GetNote()
        {
            throw new NotImplementedException();
        }

        public Response<Note> UpdateNote(int _noteId, Note _note)
        {
            throw new NotImplementedException();
        }

        public Response<Note> DeleteNote(int _noteId)
        {
            throw new NotImplementedException();
        }
    }
}