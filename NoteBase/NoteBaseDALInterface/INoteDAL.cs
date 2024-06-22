using NoteBaseDALInterface.Models;

namespace NoteBaseDALInterface
{
    public interface INoteDAL
    {
        NoteDTO Create(string _title, string _text, Guid _categoryId, Guid _personId);
        NoteDTO GetById(Guid _noteId);
        List<NoteDTO> GetByPerson(Guid _personId);
        NoteDTO GetByTitle(string _Title);
        List<NoteDTO> GetByCategory(Guid _categoryId);
        List<NoteDTO> GetByTag(Guid _tagId, Guid _personId);
        NoteDTO Update(Guid _id, string _title, string _text, Guid _categoryId);
        void Delete(Guid _noteId);
    }
}
