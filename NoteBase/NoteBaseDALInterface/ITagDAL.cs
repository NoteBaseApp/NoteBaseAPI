using NoteBaseDALInterface.Models;

namespace NoteBaseDALInterface
{
    public interface ITagDAL
    {
        TagDTO Create(string _title);
        TagDTO GetById(int _tagId);
        List<TagDTO> GetByPerson(int _PersonId);
        List<TagDTO> GetByNote(int _noteId);
        TagDTO GetByTitle(string _Title);
        TagDTO Update(int _id,string _title);
        void Delete(int _tagId);
    }
}