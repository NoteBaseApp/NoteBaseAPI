using NoteBaseDALInterface.Models;

namespace NoteBaseDALInterface
{
    public interface ICategoryDAL
    {
        CategoryDTO Create(string _title, int _personId);
        CategoryDTO GetById(int _catId);
        List<CategoryDTO> GetByPerson(int _personId);
        CategoryDTO GetByTitle(string _Title);
        CategoryDTO Update(int _id, string _title, int _personId);
        void Delete(int _catId);
    }
}