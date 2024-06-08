using NoteBaseDALInterface.Models;

namespace NoteBaseDALInterface
{
    public interface ITagDAL
    {
        TagDTO Create(string _title);
        void CreateNoteTag(Guid _noteId, Guid _tagId);
        TagDTO GetById(Guid _tagId);
        List<TagDTO> GetByPerson(Guid _PersonId);
        List<TagDTO> GetByNote(Guid _noteId);
        TagDTO GetByTitle(string _Title);
        TagDTO Update(Guid _id,string _title);
        void Delete(Guid _tagId);
        void DeleteNoteTag(Guid _noteId);
    }
}