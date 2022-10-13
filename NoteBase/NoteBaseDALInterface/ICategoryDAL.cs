using NoteBaseDALInterface.Models;

namespace NoteBaseDALInterface
{
    public interface ICategoryDAL
    {
        DALResponse<CategoryDTO> Create(CategoryDTO _cat);
        DALResponse<CategoryDTO> Delete(int _catId);
        DALResponse<CategoryDTO> Get(int _catId);
        DALResponse<CategoryDTO> Get(string _userId);
        DALResponse<CategoryDTO> GetByTitle(string _Title);
        DALResponse<CategoryDTO> Update(int _catId, CategoryDTO _cat);
    }
}