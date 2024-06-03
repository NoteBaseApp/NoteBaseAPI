using NoteBaseLogicInterface.Models;
using System.ComponentModel;

namespace NoteBaseAPI.Models
{
    public class NoteModel
    {
        private readonly List<TagModel> tagList = new();

        public int ID { get; set; }
        public string Title { get; private set; }
        public string Text { get; private set; }
        public int CategoryId { get; private set; }
        public IReadOnlyList<TagModel> TagList { get { return tagList; } }
        public int PersonId { get; set; }

        public NoteModel(Note _note)
        {
            ID = _note.ID;
            Title = _note.Title;
            Text = _note.Text;
            CategoryId = _note.CategoryId;

            foreach (Tag tag in _note.tagList)
            {
                tagList.Add(new(tag));
            }
        }
    }
}
