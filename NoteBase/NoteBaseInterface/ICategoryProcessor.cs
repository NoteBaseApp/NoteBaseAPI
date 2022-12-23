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
        Category Create(string _title, int _personId);
        Category GetById(int _catId);
        List<Category> GetByPerson(int _personId);
        Category GetByTitle(string _title);
        Category Update(int _id, string _title,int _personId);
        void Delete(int _catId);
    }
}
