using NoteBaseLogicInterface.Models;
using System.ComponentModel;

namespace NoteBaseAPI.Models
{
    public class TagModel
    {
        public Guid ID { get; set; }

        [DisplayName("Titel")]
        public string Title { get; private set; }

        public TagModel(Tag _tag)
        {
            ID = _tag.ID;
            Title = _tag.Title;
        }
    }
}
