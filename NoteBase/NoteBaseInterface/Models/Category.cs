namespace NoteBaseLogicInterface.Models
{
    public class Category
    {
        public int ID { get; }
        public string Title { get; private set; }

        public Category(int _id, string _title)
        {
            ID = _id;
            Title = _title;
        }
    }
}