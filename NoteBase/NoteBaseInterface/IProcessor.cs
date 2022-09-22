using NoteBaseLogicInterface.Models;

namespace NoteBaseInterface
{
    public interface IProcessor<T>
    {
        public Response<T> Create(T _object);
        public Response<T> Get(int _objectId);
        public Response<T> Get(string _UserMail);
        public Response<T> Update(int _objectId, T _object);
        public Response<T> Delete(int _objectId);
    }
}