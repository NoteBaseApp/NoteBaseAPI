using NoteBaseDALInterface.Models;

namespace NoteBaseDALInterface
{
    public interface IDAL<T>
    {
        public DALResponse<T> Create(T _object);
        public DALResponse<T> Get(int _objecId);
        public DALResponse<T> Get(string _userMail);
        public DALResponse<T> Update(int _objectId, T _object);
        public DALResponse<T> Delete(int _objecId);
    }
}