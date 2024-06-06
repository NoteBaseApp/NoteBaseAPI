using NoteBaseLogicInterface.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteBaseLogicInterface
{
    public interface ICategoryProcessor
    {
        bool IsValidTitle(string _title);
        bool IsTitleUnique(string _title);
        bool DoesCategoryExits(Guid _id);
        Category Create(string _title, Guid _personId);
        Category GetById(Guid _catId);
        List<Category> GetByPerson(Guid _personId);
        Category GetByTitle(string _title);
        Category Update(Guid _id, string _title, Guid _personId);
        void Delete(Guid _catId);
    }
}
