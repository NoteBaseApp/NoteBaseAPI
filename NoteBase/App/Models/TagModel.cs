using NoteBaseLogicInterface.Models;

namespace App.Models
{
    public class TagModel
    {
        public int ID { get; set; }
        public string Title { get; private set; }

        public TagModel(string _title)
        {
            Title = _title;
        }

        public Tag ToLogicModel()
        {
            return new Tag(ID, Title);
        }
    }
}
