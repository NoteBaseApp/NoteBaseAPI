using NoteBaseLogicInterface.Models;

namespace App.Models
{
    public class TagModel
    {
        public int ID { get; set; }
        public string Title { get; private set; }

        public TagModel(int _id, string _title)
        {
            ID = _id;
            Title = _title;
        }

        public Tag ToLogicModel()
        {
            return new Tag(ID, Title);
        }
    }
}
