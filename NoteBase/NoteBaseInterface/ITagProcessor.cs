using NoteBaseDALInterface.Models;
using NoteBaseLogicInterface.Models;

namespace NoteBaseInterface
{
    public interface ITagProcessor
    {
        void CreateTags(string _text, Guid NoteId);
        void UpdateTags(Guid _noteId, string _text, Guid _personId, List<Tag> _tags);
        bool IsValidTitle(string _title);
        Tag Create(string _title);
        Tag GetById(Guid _tagId);
        List<Tag> GetByPerson(Guid _PersonId);
        Tag GetByTitle(string _Title);
        void DeleteWhenUnused(Guid _tagId, Guid _PersonId);
        void DeleteNoteTag(Guid _noteId);
    }
}