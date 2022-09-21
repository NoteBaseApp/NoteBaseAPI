using NoteBaseDALInterface;
using NoteBaseDALInterface.Models;

namespace NoteBaseDAL
{
    public class NoteDAL : IDAL<NoteDTO>
    {
        private readonly string ConnString;

        public NoteDAL(string _connString)
        {
            ConnString = _connString;
        }

        public DALResponse<NoteDTO> Create(NoteDTO _note)
        {
            throw new NotImplementedException();
        }

        public DALResponse<NoteDTO> Delete(int _noteId)
        {
            throw new NotImplementedException();
        }

        public DALResponse<NoteDTO> Get(int _noteId)
        {
            throw new NotImplementedException();
        }

        public DALResponse<NoteDTO> Get()
        {
            throw new NotImplementedException();
        }

        public DALResponse<NoteDTO> Update(int _noteId, NoteDTO _note)
        {
            throw new NotImplementedException();
        }
    }
}