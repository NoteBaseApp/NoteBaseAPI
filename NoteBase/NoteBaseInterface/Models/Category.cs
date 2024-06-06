using NoteBaseDALInterface.Models;

namespace NoteBaseLogicInterface.Models
{
    public class Category
    {
        public Guid ID { get; }
        public string Title { get; private set; }
        public Guid PersonId { get; private set; }
        public List<Note> noteList { get; set; } = new();

        public Category(Guid _id, string _title, Guid _personId)
        {
            ID = _id;
            Title = _title;
            PersonId = _personId;
        }
    }
}