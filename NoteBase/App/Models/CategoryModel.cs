using NoteBaseLogicInterface.Models;

namespace App.Models
{
    public class CategoryModel
    {
        public int ID { get; set; }
        public string Title { get; private set; }
        public int PersonId { get; set; }

        public CategoryModel(string _title)
        {
            Title = _title;
        }

        public Category ToLogicModel()
        {
            Category category = new(ID, Title, PersonId);

            return category;
        }
    }
}
