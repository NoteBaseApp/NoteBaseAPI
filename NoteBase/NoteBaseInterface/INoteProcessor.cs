using NoteBaseDALInterface.Models;
using NoteBaseLogicInterface.Models;

namespace NoteBaseLogicInterface
{
    public interface INoteProcessor
    {
        public bool IsValidTitle(string _title);
        public bool IsValidText(string _title);
        public bool IsTitleUnique(string _title);
        public bool DoesNoteExits(int _id);
        Note Create(string _title, string _text, int _categoryId, int _personId);
        Note GetById(int _noteId);
        List<Note> GetByPerson(int _personId);
        Note GetByTitle(string _Title);
        List<Note> GetByCategory(int _categoryId);
        List<Note> GetByTag(int _tagId);
        Note Update(int _id, string _title, string _text, int _categoryId, int _personId, List<Tag> _tags);
        void Delete(Note _note, int _PersonId);
    }
}