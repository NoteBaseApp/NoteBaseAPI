using NoteBaseLogicInterface.Models;

namespace NoteBaseInterface
{
    public interface INoteProcessor
    {
        Response<Note> Create(Note _note);
        Response<Note> Delete(int _noteId);
        Response<Note> Get(int _noteId);
        Response<Note> Get(string _UserMail);
        Response<Note> Update(int _noteId, Note _note);
    }
}