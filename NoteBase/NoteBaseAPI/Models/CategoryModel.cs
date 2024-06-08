using NoteBaseDALInterface.Models;
using NoteBaseLogicInterface.Models;
using System.ComponentModel;
using System.Xml.Linq;

namespace NoteBaseAPI.Models
{
    public class CategoryModel
    {
        private readonly List<NoteModel> noteList = new();

        public Guid ID { get; set; }

        [DisplayName("Titel")]
        public string Title { get; private set; }
        public IReadOnlyList<NoteModel> NoteList { get { return noteList; } }
        public Guid PersonId { get; set; }

        public CategoryModel(Category _category)
        {
            ID = _category.ID;
            Title = _category.Title;
            PersonId = _category.PersonId;

            foreach (Note note in _category.noteList)
            {
                noteList.Add(new(note));
            }
        }
    }
}
