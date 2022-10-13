using NoteBaseDALInterface;
using NoteBaseDALInterface.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteBaseDAL
{
    public class CategoryDAL : ICategoryDAL
    {
        private readonly string ConnString;

        public CategoryDAL(string _connString)
        {
            ConnString = _connString;
        }

        public DALResponse<CategoryDTO> Create(CategoryDTO _cat)
        {
            throw new NotImplementedException();
        }
        public DALResponse<CategoryDTO> Get(int _catId)
        {
            throw new NotImplementedException();
        }

        public DALResponse<CategoryDTO> Get(string _userId)
        {
            throw new NotImplementedException();
        }

        public DALResponse<CategoryDTO> GetByTitle(string _Title)
        {
            throw new NotImplementedException();
        }

        public DALResponse<CategoryDTO> Update(int _catId, CategoryDTO _cat)
        {
            throw new NotImplementedException();
        }

        public DALResponse<CategoryDTO> Delete(int _catId)
        {
            throw new NotImplementedException();
        }

    }
}
