namespace NoteBaseDALInterface.Models
{
    public class NoteDTO
    {
        public int ID { get; }
        public string Title { get; private set; }
        public string Text { get; private set; }
        public int CategoryId { get; private set; }
        public readonly List<TagDTO> tagList = new();
        public int PersonId { get; set; }

        public NoteDTO(int _id, string _title, string _text, int _categoryId, int _personId)
        {
            ID = _id;
            Title = _title;
            Text = _text;
            CategoryId = _categoryId;
            PersonId = _personId;
        }
    }
}