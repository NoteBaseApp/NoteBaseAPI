using NoteBaseDALInterface.Models;

namespace NoteBaseLogicInterface.Models
{
    public class Tag
    {
        public Guid ID { get; }
        public string Title { get; private set; }

        public Tag(Guid _id, string _title)
        {
            ID = _id;
            Title = _title;
        }
    }
}