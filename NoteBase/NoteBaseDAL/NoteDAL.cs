using NoteBaseDALInterface;
using NoteBaseDALInterface.Models;

namespace NoteBaseDAL
{
    public class NoteDAL : INoteDAL
    {
        private readonly string ConnString;

        public NoteDAL(string _connString)
        {
            ConnString = _connString;
        }

        public DALResponse<NoteDTO> CreateNote(NoteDTO _note)
        {
            throw new NotImplementedException();
        }

        public DALResponse<NoteDTO> DeleteNote(int _noteId)
        {
            throw new NotImplementedException();
        }

        public DALResponse<NoteDTO> GetNote(int _noteId)
        {
            throw new NotImplementedException();
        }

        public DALResponse<NoteDTO> GetNote()
        {
            throw new NotImplementedException();
        }

        public DALResponse<NoteDTO> UpdateNote(int _noteId, NoteDTO _note)
        {
            throw new NotImplementedException();
        }
    }
}