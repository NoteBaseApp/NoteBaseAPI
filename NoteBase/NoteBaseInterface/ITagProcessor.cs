using NoteBaseDALInterface.Models;
using NoteBaseLogicInterface.Models;

namespace NoteBaseInterface
{
    public interface ITagProcessor
    {
        void CreateTags(string _text, int NoteId);
        void UpdateTags(int _noteId, string _text, int _personId, List<Tag> _tags);
        bool IsValidTitle(string _title);
        Tag Create(string _title);
        Tag GetById(int _tagId);
        List<Tag> GetByPerson(int _PersonId);
        Tag GetByTitle(string _Title);
        void TryDelete(int _tagId, int _PersonId);
        void DeleteNoteTag(int _noteId);
    }
}