using NoteBaseDALInterface;
using NoteBaseDALInterface.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteBaseLogicTests.TestDALs
{
    internal class CreateTest_SucceedDAL : ICategoryDAL
    {
        public DALResponse<CategoryDTO> Create(CategoryDTO _cat)
        {
            return new(true);
        }

        public DALResponse<CategoryDTO> Delete(int _catId)
        {
            throw new NotImplementedException();
        }

        public DALResponse<CategoryDTO> GetById(int _catId)
        {
            throw new NotImplementedException();
        }

        public DALResponse<CategoryDTO> GetByPerson(int _personId)
        {
            throw new NotImplementedException();
        }

        public DALResponse<CategoryDTO> Update(CategoryDTO _cat)
        {
            throw new NotImplementedException();
        }
    }
}
