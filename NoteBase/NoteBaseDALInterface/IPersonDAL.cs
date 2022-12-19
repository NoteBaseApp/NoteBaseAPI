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
        public int Create(PersonDTO _person);
        public PersonDTO GetByEmail(string _personEmail);
        public int Update(PersonDTO _person);
        public int Delete(int _personId);
    }
}
