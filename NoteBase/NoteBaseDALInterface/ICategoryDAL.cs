using NoteBaseDALInterface.Models;

namespace NoteBaseDALInterface
{
    public interface ICategoryDAL
    {
        int Create(CategoryDTO _cat);
        CategoryDTO GetById(int _catId);
        List<CategoryDTO> GetByPerson(int _personId);
        CategoryDTO GetByTitle(string _Title);
        int Update(CategoryDTO _cat);
        int Delete(int _catId);
    }
}