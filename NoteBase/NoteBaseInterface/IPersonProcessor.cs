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
        Response<Person> Create(Person _person);
        Response<Person> GetByEmail(string _email);
        Response<Person> Update(Person _person);
        Response<Person> Delete(int _personId);
    }
}
