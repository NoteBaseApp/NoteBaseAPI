using NoteBaseDALInterface.Models;

namespace NoteBaseDALInterface
{
    public interface INoteDAL
    {
        NoteDTO Create(string _title, string _text, int _categoryId, int _personId);
        NoteDTO GetById(int _noteId);
        List<NoteDTO> GetByPerson(int _personId);
        NoteDTO GetByTitle(string _Title);
        List<NoteDTO> GetByCategory(int _categoryId);
        List<NoteDTO> GetByTag(int _tagId);
        NoteDTO Update(int _id, string _title, string _text, int _categoryId);
        void Delete(int _noteId);
    }
}
