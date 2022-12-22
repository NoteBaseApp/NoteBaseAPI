using NoteBaseDALInterface.Models;

namespace NoteBaseDALInterface
{
    public interface ITagDAL
    {
        int Create(string _title);
        TagDTO GetById(int _tagId);
        List<TagDTO> GetByPerson(int _PersonId);
        List<TagDTO> GetByNote(int _noteId);
        TagDTO GetByTitle(string _Title);
        int Update(int _id,string _title);
        int Delete(int _tagId);
    }
}