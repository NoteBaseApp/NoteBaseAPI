using NoteBaseDALInterface.Models;
using NoteBaseLogicInterface.Models;

namespace NoteBaseInterface
{
    public interface ITagProcessor
    {
        int Create(Tag _tag);
        Tag GetById(int _tagId);
        List<Tag> GetByPerson(int _PersonId);
        Tag GetByTitle(string _Title);
        int Update(Tag _tag);
        int TryDelete(int _tagId, int _PersonId);
    }
}