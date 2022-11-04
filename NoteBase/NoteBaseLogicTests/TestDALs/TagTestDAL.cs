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
            throw new NotImplementedException();
        }

        public DALResponse<TagDTO> Delete(int _tagId)
        {
            throw new NotImplementedException();
        }

        public DALResponse<TagDTO> Get(int _tagId)
        {
            throw new NotImplementedException();
        }

        public DALResponse<TagDTO> Get(string _userMail)
        {
            throw new NotImplementedException();
        }

        public DALResponse<TagDTO> GetByTitle(string _Title)
        {
            throw new NotImplementedException();
        }

        public DALResponse<TagDTO> GetFromNote(int _noteId)
        {
            throw new NotImplementedException();
        }

        public DALResponse<TagDTO> Update(int _tagId, TagDTO _tag)
        {
            throw new NotImplementedException();
        }
    }
}
