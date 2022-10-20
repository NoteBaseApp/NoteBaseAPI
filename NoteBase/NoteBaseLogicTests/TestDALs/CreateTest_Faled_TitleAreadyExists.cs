using NoteBaseDALInterface;
using NoteBaseDALInterface.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteBaseLogicTests.TestDALs
{
    internal class CreateTest_Faled_TitleAreadyExists : ICategoryDAL
    {
        public DALResponse<CategoryDTO> Create(CategoryDTO _cat)
        {
            DALResponse<CategoryDTO> response = new DALResponse<CategoryDTO>(false);
            response.Code = 2627;

            return response;
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
