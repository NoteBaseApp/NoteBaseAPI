using NoteBaseLogicInterface.Models;

namespace App.Models
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
