using NoteBaseDALInterface.Models;

namespace NoteBaseDALInterface
{
    public interface INoteDAL
    {
        DALResponse<NoteDTO> Create(NoteDTO _note);
        DALResponse<NoteDTO> CreateNoteTag(int _noteId, int _tagId);
        DALResponse<NoteDTO> GetById(int _noteId);
        DALResponse<NoteDTO> GetByPerson(int _personId);
        DALResponse<NoteDTO> GetByTitle(string _Title);
        DALResponse<NoteDTO> GetByCategory(int _categoryId);
        DALResponse<NoteDTO> Update(NoteDTO _note);
        DALResponse<NoteDTO> Delete(int _noteId);
        DALResponse<NoteDTO> DeleteNoteTag(int _noteId);
    }
}
