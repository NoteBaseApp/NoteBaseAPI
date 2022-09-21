namespace NoteBaseDALInterface.Models
{
    public class TagDTO
    {
        public int ID { get; }
        public string Title { get; private set; }

        public TagDTO(int _id, string _title)
        {
            ID = _id;
            Title = _title;
        }
    }
}