using NoteBaseDALInterface.Models;

namespace NoteBaseDALInterface
{
    public interface ICategoryDAL
    {
        DALResponse<CategoryDTO> Create(CategoryDTO _cat);
        DALResponse<CategoryDTO> Delete(int _catId);
        DALResponse<CategoryDTO> GetById(int _catId);
        DALResponse<CategoryDTO> GetByPerson(int _personId);
        //DALResponse<CategoryDTO> GetByTitle(string _Title);
        DALResponse<CategoryDTO> Update(CategoryDTO _cat);
    }
}