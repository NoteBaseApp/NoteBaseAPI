using NoteBaseDALInterface.Models;
using NoteBaseLogicInterface.Models;

namespace NoteBaseInterface
{
    public interface ITagProcessor
    {
        Response<Tag> Create(Tag _tag);
        Response<Tag> Delete(int _tagId);
        Response<Tag> GetById(int _tagId);
        Response<Tag> GetByPerson(string _PersonMail);
        Response<Tag> GetByTitle(string _Title);
        Response<Tag> Update(Tag _tag);
    }
}