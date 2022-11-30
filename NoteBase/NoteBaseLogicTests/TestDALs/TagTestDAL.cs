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
        public DALResponse<TagDTO> Create(TagDTO _tag)
        {
            return new(true);
        }

        public DALResponse<TagDTO> Delete(int _tagId)
        {
            throw new NotImplementedException();
        }

        public DALResponse<TagDTO> GetById(int _tagId)
        {
            throw new NotImplementedException();
        }

        public DALResponse<TagDTO> GetByPerson(string _userMail)
        {
            throw new NotImplementedException();
        }

        public DALResponse<TagDTO> GetByTitle(string _Title)
        {
            DALResponse<TagDTO> response = new(true);

            if (_Title == "fontys")
            {
                response.AddItem(new(11, "fontys"));
            } 
            else if (_Title == "eindhoven")
            {
                response.AddItem(new(12, "fontys"));
            }

            return response;
        }

        public DALResponse<TagDTO> GetByNote(int _noteId)
        {
            throw new NotImplementedException();
        }

        public DALResponse<TagDTO> Update(int _tagId, TagDTO _tag)
        {
            throw new NotImplementedException();
        }

        public DALResponse<TagDTO> Update(TagDTO _tag)
        {
            throw new NotImplementedException();
        }
    }
}
