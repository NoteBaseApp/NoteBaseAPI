using NoteBaseDALInterface.Models;

namespace NoteBaseDALInterface
{
    public interface INoteDAL
    {
        int Create(string _title, string _text, int _categoryId, int _personId);
        int CreateNoteTag(int _noteId, int _tagId);
        NoteDTO GetById(int _noteId);
        List<NoteDTO> GetByPerson(int _personId);
        NoteDTO GetByTitle(string _Title);
        List<NoteDTO> GetByCategory(int _categoryId);
        List<NoteDTO> GetByTag(int _tagId);
        int Update(int _id, string _title, string _text, int _categoryId);
        int Delete(int _noteId);
        int DeleteNoteTag(int _noteId);
    }
}
