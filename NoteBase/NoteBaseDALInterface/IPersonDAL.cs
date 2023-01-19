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
        public int Create(string _name, string _email);
        public PersonDTO GetByEmail(string _personEmail);
        public int Update(int _id, string _name, string _email);
        public int Delete(int _personId);
    }
}
