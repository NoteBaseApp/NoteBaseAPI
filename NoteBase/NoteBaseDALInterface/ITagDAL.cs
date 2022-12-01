using NoteBaseDALInterface.Models;

namespace NoteBaseDALInterface
{
    public interface ITagDAL
    {
        DALResponse<TagDTO> Create(TagDTO _tag);
        DALResponse<TagDTO> GetById(int _tagId);
        DALResponse<TagDTO> GetByPerson(int _PersonId);
        DALResponse<TagDTO> GetByNote(int _noteId);
        DALResponse<TagDTO> GetByTitle(string _Title);
        DALResponse<TagDTO> Update(TagDTO _tag);
        DALResponse<TagDTO> Delete(int _tagId);
    }
}