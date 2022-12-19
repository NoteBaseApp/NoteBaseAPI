using NoteBaseDALInterface;
using NoteBaseDALInterface.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteBaseLogicTests.TestDALs
{
    internal class TagTestDAL : ITagDAL
    {
        public int Create(TagDTO _tag)
        {
            return 1;
        }

        public int Delete(int _tagId)
        {
            throw new NotImplementedException();
        }

        public TagDTO GetById(int _tagId)
        {
            throw new NotImplementedException();
        }

        public List<TagDTO> GetByPerson(string _userMail)
        {
            throw new NotImplementedException();
        }

        public TagDTO GetByTitle(string _Title)
        {
            if (_Title == "fontys")
            {
                return new(11, "fontys");
            } 
            else if (_Title == "eindhoven")
            {
                return new(12, "fontys");
            }

            return new(0, "");
        }

        public List<TagDTO> GetByNote(int _noteId)
        {
            throw new NotImplementedException();
        }

        public int Update(int _tagId, TagDTO _tag)
        {
            throw new NotImplementedException();
        }

        public int Update(TagDTO _tag)
        {
            throw new NotImplementedException();
        }

        public List<TagDTO> GetByPerson(int _PersonId)
        {
            throw new NotImplementedException();
        }
    }
}
