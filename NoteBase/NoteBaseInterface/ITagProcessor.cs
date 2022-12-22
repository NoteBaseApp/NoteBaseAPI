using NoteBaseDALInterface.Models;
using NoteBaseLogicInterface.Models;

namespace NoteBaseInterface
{
    public interface ITagProcessor
    {
        int Create(string _title);
        Tag GetById(int _tagId);
        List<Tag> GetByPerson(int _PersonId);
        Tag GetByTitle(string _Title);
        int Update(int _tagId, string _title);
        int TryDelete(int _tagId, int _PersonId);
    }
}