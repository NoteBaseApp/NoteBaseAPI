using NoteBaseLogicInterface.Models;
using System.ComponentModel;

namespace App.Models
{
    public class NoteModel
    {
        private readonly List<TagModel> tagList = new();

        public int ID { get; set; }

        [DisplayName("Titel")]
        public string Title { get; private set; }

        [DisplayName("Tekst")]
        public string Text { get; private set; }

        [DisplayName("Categorie")]
        public int CategoryId { get; private set; }
        public IReadOnlyList<TagModel> TagList { get { return tagList; } }
        public int PersonId { get; set; }

        public NoteModel(Note _note)
        {
            ID = _note.ID;
            Title = _note.Title;
            Text = _note.Text;
            CategoryId = _note.CategoryId;

            foreach (Tag tag in _note.TagList)
            {
                AddTag(new(tag));
            }
        }

        private void AddTag(TagModel _tag)
        {
            tagList.Add(_tag);
        }
    }
}
