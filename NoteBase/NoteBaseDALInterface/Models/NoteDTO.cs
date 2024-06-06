namespace NoteBaseDALInterface.Models
{
    public class NoteDTO
    {
        public Guid ID { get; }
        public string Title { get; private set; }
        public string Text { get; private set; }
        public Guid CategoryId { get; private set; }
        public readonly List<TagDTO> tagList = new();
        public Guid PersonId { get; set; }

        public NoteDTO(Guid _id, string _title, string _text, Guid _categoryId, Guid _personId)
        {
            ID = _id;
            Title = _title;
            Text = _text;
            CategoryId = _categoryId;
            PersonId = _personId;
        }
    }
}