using NoteBaseDALInterface.Models;
using NoteBaseLogicInterface.Models;

namespace NoteBaseLogicInterface
{
    public interface INoteProcessor
    {
        Note Create(string _title, string _text, int _categoryId, int _personId);
        Note GetById(int _noteId);
        List<Note> GetByPerson(int _personId);
        Note GetByTitle(string _Title);
        List<Note> GetByCategory(int _categoryId);
        List<Note> GetByTag(int _tagId);
        Note Update(int _id, string _title, string _text, int _categoryId, int _personId);
        int Delete(Note _note, int _PersonId);
    }
}