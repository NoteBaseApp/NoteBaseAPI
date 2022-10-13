namespace NoteBaseDALInterface.Models
{
    public class NoteDTO
    {
        private readonly List<TagDTO> tagList = new();

        public int ID { get; }
        public string Title { get; private set; }
        public string MainBody { get; private set; }
        public CategoryDTO Category { get; private set; }
        public IReadOnlyList<TagDTO> TagList { get { return tagList; } }
        public int PersonId { get; set; }

        public NoteDTO(int _id, string _title, string _mainBody, CategoryDTO _category)
        {
            ID = _id;
            Title = _title;
            MainBody = _mainBody;
            Category = _category;
            //PersonId = _PersonId;
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