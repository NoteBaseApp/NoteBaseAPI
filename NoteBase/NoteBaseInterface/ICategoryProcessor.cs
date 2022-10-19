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
        Response<Category> Create(Category _cat);
        Response<Category> GetById(int _catId);
        Response<Category> GetByPerson(int _personId);
        //Response<Category> GetByTitle(string _title);
        Response<Category> Update(Category _cat);
        Response<Category> Delete(int _catId);
    }
}
