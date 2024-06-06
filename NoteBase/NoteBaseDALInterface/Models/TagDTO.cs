namespace NoteBaseDALInterface.Models
{
    public class TagDTO
    {
        public Guid ID { get; }
        public string Title { get; private set; }

        public TagDTO(Guid _id, string _title)
        {
            ID = _id;
            Title = _title;
        }
    }
}