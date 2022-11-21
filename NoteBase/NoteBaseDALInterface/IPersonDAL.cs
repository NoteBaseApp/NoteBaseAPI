using NoteBaseDALInterface.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteBaseDALInterface
{
    public interface IPersonDAL
    {
        public DALResponse<PersonDTO> Create(PersonDTO _person);
        public DALResponse<PersonDTO> GetByEmail(string _personEmail);
        public DALResponse<PersonDTO> Update(PersonDTO _person);
        public DALResponse<PersonDTO> Delete(int _personId);
    }
}
