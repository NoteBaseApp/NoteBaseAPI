namespace NoteBaseDALInterface.Models
{
    public class NoteDTO
    {
        private readonly List<TagDTO> tagList = new();

        public int ID { get; }
        public string Title { get; private set; }
        public string Text { get; private set; }
        public int CategoryId { get; private set; }
        public IReadOnlyList<TagDTO> TagList { get { return tagList; } }
        public int PersonId { get; set; }

        public NoteDTO(int _id, string _title, string _text, int _categoryId)
        {
            ID = _id;
            Title = _title;
            Text = _text;
            CategoryId = _categoryId;
        }

        public void TryAddTagDTO(TagDTO _tagDTO)
        {
            if (tagList.Contains(_tagDTO))
            {
                throw new Exception("Tag already in List");
            }

            tagList.Add(_tagDTO);
        }
    }
}