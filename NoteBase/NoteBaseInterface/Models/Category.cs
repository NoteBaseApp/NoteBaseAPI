using NoteBaseDALInterface.Models;

namespace NoteBaseLogicInterface.Models
{
    public class Category
    {
        public int ID { get; }
        public string Title { get; private set; }
        public int PersonId { get; private set; }
        public List<Note> noteList { get; set; } = new();

        public Category(int _id, string _title, int _personId)
        {
            ID = _id;
            Title = _title;
            PersonId = _personId;
        }
    }
}