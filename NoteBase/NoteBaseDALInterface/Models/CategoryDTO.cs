namespace NoteBaseDALInterface.Models
{
    public class CategoryDTO
    {
        public int ID { get; }
        public string Title { get; private set; }

        public CategoryDTO(int _id, string _title)
        {
            ID = _id;
            Title = _title;
        }
    }
}