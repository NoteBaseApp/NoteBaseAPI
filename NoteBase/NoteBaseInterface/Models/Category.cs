using NoteBaseDALInterface.Models;

namespace NoteBaseLogicInterface.Models
{
    public class Category
    {
        public int ID { get; }
        public string Title { get; private set; }
        public int PersonId { get; private set; }

        public Category(int _id, string _title, int _personId)
        {
            ID = _id;
            Title = _title;
            PersonId = _personId;
        }

        public CategoryDTO ToDTO()
        {
            CategoryDTO categoryDTO = new(ID, Title, PersonId);

            return categoryDTO;
        }
    }
}