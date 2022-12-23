using NoteBaseDALInterface.Models;
using NoteBaseLogicInterface.Models;

namespace NoteBaseInterface
{
    public interface ITagProcessor
    {
        Tag Create(string _title);
        Tag GetById(int _tagId);
        List<Tag> GetByPerson(int _PersonId);
        Tag GetByTitle(string _Title);
        Tag Update(int _tagId, string _title);
        void TryDelete(int _tagId, int _PersonId);
    }
}