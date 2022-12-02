using NoteBaseLogicInterface.Models;
using System.ComponentModel;

namespace App.Models
{
    public class TagModel
    {
        public int ID { get; set; }

        [DisplayName("Titel")]
        public string Title { get; private set; }

        public TagModel(int _id, string _title)
        {
            ID = _id;
            Title = _title;
        }

        public TagModel(Tag _tag)
        {
            ID = _tag.ID;
            Title = _tag.Title;
        }

        public Tag ToLogicModel()
        {
            return new Tag(ID, Title);
        }
    }
}
