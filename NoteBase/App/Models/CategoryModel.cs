using NoteBaseDALInterface.Models;
using NoteBaseLogicInterface.Models;
using System.ComponentModel;

namespace App.Models
{
    public class CategoryModel
    {
        private readonly List<NoteModel> noteList = new();

        public int ID { get; set; }

        [DisplayName("Titel")]
        public string Title { get; private set; }
        public IReadOnlyList<NoteModel> NoteList { get { return noteList; } }
        public int PersonId { get; set; }

        public CategoryModel(Category _category)
        {
            ID = _category.ID;
            Title = _category.Title;
            PersonId = _category.PersonId;

            foreach (Note note in _category.NoteList)
            {
                AddNote(new(note));
            }
        }

        private void AddNote(NoteModel _note)
        {
            noteList.Add(_note);
        }
    }
}
