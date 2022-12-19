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
        Category Create(Category _cat);
        Category GetById(int _catId);
        List<Category> GetByPerson(int _personId);
        Category GetByTitle(string _title);
        Category Update(Category _cat);
        int Delete(int _catId);
    }
}
