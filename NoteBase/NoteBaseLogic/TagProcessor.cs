using NoteBaseDALInterface.Models;
using NoteBaseDALInterface;
using NoteBaseInterface;
using NoteBaseLogicInterface.Models;

namespace NoteBaseLogic
{
    public class TagProcessor : ITagProcessor
    {
        private readonly ITagDAL TagDAL;

        public TagProcessor(ITagDAL _tagDAL)
        {
            TagDAL = _tagDAL;
        }

        public int Create(string _title)
        {
            if (_title == "")
            {
                throw new ArgumentException("Title can't be empty");
            }

            return TagDAL.Create(_title);
        }

        public Tag GetById(int _tagId)
        {
            TagDTO tagDTO = TagDAL.GetById(_tagId);

            return new(tagDTO);
        }

        //need to remake this for using person id
        public List<Tag> GetByPerson(int _personId)
        {
            List<Tag> result = new();

            List<TagDTO> tagDTOs = TagDAL.GetByPerson(_personId);
            foreach (TagDTO tagDTO in tagDTOs)
            {
                result.Add(new(tagDTO));
            }

            return result;
        }

        public Tag GetByTitle(string _title)
        {
            TagDTO tagDTO = TagDAL.GetByTitle(_title);

            return new(tagDTO);
        }

        public int Update(int _id, string _title)
        {
            if (_title == "")
            {
                throw new ArgumentException("Title can't be empty");
            }

            return TagDAL.Update(_id, _title);
        }

        public int TryDelete(int _tagId, int _PersonId)
        {
            List<Tag> Tags = GetByPerson(_PersonId);

            foreach (Tag tag in Tags)
            {
                if (tag.ID == _tagId)
                {
                    return 0;
                }
            }

            return TagDAL.Delete(_tagId);
        }
    }
}
