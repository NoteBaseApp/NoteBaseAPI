using NoteBaseDALInterface.Models;

namespace NoteBaseDALInterface
{
    public interface ICategoryDAL
    {
        int Create(string _title, int _personId);
        CategoryDTO GetById(int _catId);
        List<CategoryDTO> GetByPerson(int _personId);
        CategoryDTO GetByTitle(string _Title);
        int Update(int _id, string _title);
        int Delete(int _catId);
    }
}