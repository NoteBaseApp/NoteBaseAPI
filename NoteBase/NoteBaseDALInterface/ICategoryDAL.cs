using NoteBaseDALInterface.Models;

namespace NoteBaseDALInterface
{
    public interface ICategoryDAL
    {
        CategoryDTO Create(string _title, Guid _personId);
        CategoryDTO GetById(Guid _catId);
        List<CategoryDTO> GetByPerson(Guid _personId);
        CategoryDTO GetByTitle(string _Title);
        CategoryDTO Update(Guid _id, string _title, Guid _personId);
        void Delete(Guid _catId);
    }
}