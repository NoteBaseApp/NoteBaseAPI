using NoteBaseLogicInterface.Models;

namespace NoteBaseLogicInterface
{
    public interface INoteProcessor
    {
        Response<Note> Create(Note _note);
        Response<Note> Delete(int _noteId);
        Response<Note> Get(int _noteId);
        Response<Note> GetByPerson(int _personId);
        Response<Note> Update(int _noteId, Note _note);
    }
}