using NoteBaseLogicInterface.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteBaseLogicInterface
{
    public interface IPersonProcessor
    {
        int Create(Person _person);
        Person GetByEmail(string _email);
        int Update(Person _person);
        int Delete(int _personId);
    }
}
