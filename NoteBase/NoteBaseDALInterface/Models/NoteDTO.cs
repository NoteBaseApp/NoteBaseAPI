namespace NoteBaseDALInterface.Models
{
    public class NoteDTO
    {
        private readonly List<TagDTO> tagList = new();

        public int ID { get; }
        public string Title { get; private set; }
        public string Text { get; private set; }
        public CategoryDTO Category { get; private set; }
        public IReadOnlyList<TagDTO> TagList { get { return tagList; } }
        public int PersonId { get; set; }

        public NoteDTO(int _id, string _title, string _text, CategoryDTO _category)
        {
            ID = _id;
            Title = _title;
            Text = _text;
            Category = _category;
            //PersonId = _PersonId;
        }

        public void TryAddTagDTO(TagDTO _tagDTO)
        {
            if (!IsTagCompatible(_tagDTO))
            {
                throw new Exception("Tag already in List");
            }

            tagList.Add(_tagDTO);
        }

        public bool IsTagCompatible(TagDTO _tag)
        {
            return !tagList.Contains(_tag);
        }
    }
}