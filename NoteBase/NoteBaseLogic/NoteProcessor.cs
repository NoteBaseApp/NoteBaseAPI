using NoteBaseDALFactory;
using NoteBaseDALInterface;
using NoteBaseDALInterface.Models;
using NoteBaseInterface;
using NoteBaseLogicInterface.Models;

namespace NoteBaseLogic
{
    public class NoteProcessor : IProcessor<Note>
    {
        private readonly IDAL<NoteDTO> NoteDAL;
        public NoteProcessor(string _connString)
        {
            NoteDAL = Factory.CreateNoteDAL(_connString);
        }

        public Response<Note> Create(Note _note)
        {
            throw new NotImplementedException();
        }

        public Response<Note> Get(int _noteId)
        {
            throw new NotImplementedException();
        }

        public Response<Note> Get(string _UserMail)
        {
            throw new NotImplementedException();
        }

        public Response<Note> Update(int _noteId, Note _note)
        {
            throw new NotImplementedException();
        }

        public Response<Note> Delete(int _noteId)
        {
            throw new NotImplementedException();
        }
    }
}