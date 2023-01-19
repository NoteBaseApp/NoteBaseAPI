using NoteBaseDALInterface.Models;

namespace NoteBaseDALInterface
{
    public interface ITagDAL
    {
        TagDTO Create(string _title);
        void CreateNoteTag(int _noteId, int _tagId);
        TagDTO GetById(int _tagId);
        List<TagDTO> GetByPerson(int _PersonId);
        List<TagDTO> GetByNote(int _noteId);
        TagDTO GetByTitle(string _Title);
        TagDTO Update(int _id,string _title);
        void Delete(int _tagId);
        void DeleteNoteTag(int _noteId);
    }
}