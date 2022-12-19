using NoteBaseDALInterface.Models;
using NoteBaseLogicInterface.Models;

namespace NoteBaseLogicInterface
{
    public interface INoteProcessor
    {
        Note Create(Note _note);
        Note GetById(int _noteId);
        List<Note> GetByPerson(int _personId);
        Note GetByTitle(string _Title);
        List<Note> GetByCategory(int _categoryId);
        List<Note> GetByTag(int _tagId);
        Note Update(Note _note);
        int Delete(Note _note, int _PersonId);
    }
}