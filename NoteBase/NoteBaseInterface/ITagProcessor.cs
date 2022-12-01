using NoteBaseDALInterface.Models;
using NoteBaseLogicInterface.Models;

namespace NoteBaseInterface
{
    public interface ITagProcessor
    {
        Response<Tag> Create(Tag _tag);
        Response<Tag> GetById(int _tagId);
        Response<Tag> GetByPerson(int _PersonId);
        Response<Tag> GetByTitle(string _Title);
        Response<Tag> Update(Tag _tag);
        Response<Tag> TryDelete(int _tagId, int _PersonId);
    }
}