using NoteBaseDALInterface.Models;

namespace NoteBaseDALInterface
{
    public interface ITagDAL
    {
        int Create(TagDTO _tag);
        TagDTO GetById(int _tagId);
        List<TagDTO> GetByPerson(int _PersonId);
        List<TagDTO> GetByNote(int _noteId);
        TagDTO GetByTitle(string _Title);
        int Update(TagDTO _tag);
        int Delete(int _tagId);
    }
}