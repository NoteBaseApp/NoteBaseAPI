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

        public NoteModel(int _id, string _title, string _text, int _categoryId)
        {
            ID = _id;
            Title = _title;
            Text = _text;
            CategoryId = _categoryId;
        }

        public Note ToLogicModel()
        {
            Note note = new Note(ID, Title, Text, CategoryId);
            note.PersonId = PersonId;

            foreach (TagModel tagModel in tagList)
            {
                Tag tag = tagModel.ToLogicModel();

                note.TryAddTag(tag);
            }

            return note;
        }

        public void AddTag(TagModel _tag)
        {
            tagList.Add(_tag);
        }
    }
}
