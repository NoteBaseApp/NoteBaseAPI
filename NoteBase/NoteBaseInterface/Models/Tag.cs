using NoteBaseDALInterface.Models;

namespace NoteBaseLogicInterface.Models
{
    public class Tag
    {
        public int ID { get; }
        public string Title { get; private set; }

        public Tag(int _id, string _title)
        {
            ID = _id;
            Title = _title;
        }

        public TagDTO ToDTO()
        {
            return new TagDTO(ID, Title);
        }
    }
}