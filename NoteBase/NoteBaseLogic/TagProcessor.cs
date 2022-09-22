using NoteBaseDALFactory;
using NoteBaseDALInterface.Models;
using NoteBaseDALInterface;
using NoteBaseInterface;
using NoteBaseLogicInterface.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteBaseLogic
{
    public class TagProcessor : IProcessor<Tag>
    {
        private readonly IDAL<TagDTO> TagDAL;
        public TagProcessor(string _connString)
        {
            TagDAL = Factory.CreateTagDAL(_connString);
        }

        public Response<Tag> Create(Tag _tag)
        {
            throw new NotImplementedException();
        }

        public Response<Tag> Delete(int _tagId)
        {
            throw new NotImplementedException();
        }

        public Response<Tag> Get(int _tagId)
        {
            throw new NotImplementedException();
        }

        public Response<Tag> Get(string _UserMail)
        {
            DALResponse<TagDTO> DALreponse = TagDAL.Get(_UserMail);
            Response<Tag> response = new(DALreponse.Status, DALreponse.Message);

            foreach (TagDTO tagDTO in DALreponse.Data)
            {
                Tag tag = new(tagDTO.ID, tagDTO.Title);
                response.AddItem(tag);
            }

            return response;
        }

        public Response<Tag> Update(int _tagId, Tag _tag)
        {
            TagDTO tagDTO = new(_tag.ID, _tag.Title);

            DALResponse<TagDTO> DALreponse = TagDAL.Update(_tagId, tagDTO);

            List<TagDTO> resposeTagDTO = (List<TagDTO>)DALreponse.Data;
            Tag tag = new(resposeTagDTO[0].ID, resposeTagDTO[0].Title);

            //create response
            Response<Tag> response = new(DALreponse.Status, DALreponse.Message);
            response.AddItem(tag);

            return response;
        }
    }
}
