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
        int Create(string _name, string _email);
        Person GetByEmail(string _email);
        int Update(Guid _id, string _name, string _email);
        int Delete(Guid _personId);
    }
}
