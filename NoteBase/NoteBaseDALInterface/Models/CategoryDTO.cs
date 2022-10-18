namespace NoteBaseDALInterface.Models
{
    public class CategoryDTO
    {
        public int ID { get; }
        public string Title { get; private set; }
        public int PersonId { get; private set; }

        public CategoryDTO(int _id, string _title, int _personId)
        {
            ID = _id;
            Title = _title;
            PersonId = _personId;
        }
    }
}