using NoteBaseDALInterface.Models;

namespace NoteBaseDALInterface
{
    public interface INoteDAL
    {
        int Create(NoteDTO _note);
        int CreateNoteTag(int _noteId, int _tagId);
        NoteDTO GetById(int _noteId);
        List<NoteDTO> GetByPerson(int _personId);
        NoteDTO GetByTitle(string _Title);
        List<NoteDTO> GetByCategory(int _categoryId);
        List<NoteDTO> GetByTag(int _tagId);
        int Update(NoteDTO _note);
        int Delete(int _noteId);
        int DeleteNoteTag(int _noteId);
    }
}
