using NoteBaseDALInterface.Models;
using NoteBaseLogicInterface.Models;

namespace NoteBaseLogicInterface
{
    public interface INoteProcessor
    {
        public bool IsValidTitle(string _title);
        public bool IsValidText(string _title);
        public bool IsTitleUnique(string _title);
        public bool IsTitleUnique(string _title, Guid _id);
        public bool DoesNoteExits(Guid _id);
        Note Create(string _title, string _text, Guid _categoryId, Guid _personId);
        Note GetById(Guid _noteId);
        List<Note> GetByPerson(Guid _personId);
        Note GetByTitle(string _Title);
        List<Note> GetByCategory(Guid _categoryId);
        List<Note> GetByTag(Guid _tagId, Guid _personId);
        Note Update(Guid _id, string _title, string _text, Guid _categoryId, Guid _personId, List<Tag> _tags);
        void Delete(Guid _noteId, List<Tag> _tagList, Guid _PersonId);
    }
}