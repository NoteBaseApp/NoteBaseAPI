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
        public TagDTO Create(string _title)
        {
            return new(Guid.Parse("00000000-0000-0000-0000-000000000000"), _title);
        }

        public void Delete(Guid _tagId)
        {
            
        }

        public TagDTO GetById(Guid _tagId)
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
                return new(Guid.Parse("74f05b9d-da24-42a1-8c09-2f3e9b014c93"), "fontys");
            } 
            else if (_Title == "eindhoven")
            {
                return new(Guid.Parse("627a015d-64e4-4be7-9758-aecefaa2dc24"), "fontys");
            }

            return new(Guid.Parse("00000000-0000-0000-0000-000000000000"), "");
        }

        public List<TagDTO> GetByNote(Guid _noteId)
        {
            throw new NotImplementedException();
        }

        public TagDTO Update(Guid _id, string _title)
        {
            throw new NotImplementedException();
        }

        public List<TagDTO> GetByPerson(Guid _PersonId)
        {
            throw new NotImplementedException();
        }

        public void CreateNoteTag(Guid _noteId, Guid _tagId)
        {
            
        }

        public void DeleteNoteTag(Guid _noteId)
        {
            
        }
    }
}
