using NoteBaseLogicInterface.Models;

namespace App.Models
{
    public class NoteModel
    {
        private readonly List<TagModel> tagList = new();

        public int ID { get; set; }
        public string Title { get; private set; }
        public string Text { get; private set; }
        public CategoryModel Category { get; private set; }
        public IReadOnlyList<TagModel> TagList { get { return tagList; } }
        public int PersonId { get; set; }

        public NoteModel(string _title, string _text, CategoryModel _category)
        {
            Title = _title;
            Text = _text;
            Category = _category;
        }

        public Note ToLogicModel()
        {
            Category cat = Category.ToLogicModel();

            Note note = new Note(ID, Title, Text, cat);
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
