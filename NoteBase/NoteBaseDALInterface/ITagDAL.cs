using NoteBaseDALInterface.Models;

namespace NoteBaseDALInterface
{
    public interface ITagDAL
    {
        DALResponse<TagDTO> Create(TagDTO _tag);
        DALResponse<TagDTO> Get(int _tagId);
        DALResponse<TagDTO> Get(string _userMail);
        DALResponse<TagDTO> GetByTitle(string _Title);
        DALResponse<TagDTO> GetFromNote(int _noteId);
        DALResponse<TagDTO> Update(int _tagId, TagDTO _tag);
        DALResponse<TagDTO> Delete(int _tagId);
    }
}