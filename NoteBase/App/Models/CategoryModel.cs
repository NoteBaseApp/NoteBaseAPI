using NoteBaseDALInterface.Models;
using NoteBaseLogicInterface.Models;
using System.ComponentModel;
using System.Diagnostics;
using static System.Net.Mime.MediaTypeNames;

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

        public CategoryModel(int _id, string _title, int _personId)
        {
            ID = _id;
            Title = _title;
            PersonId = _personId;
        }

        public Category ToLogicModel()
        {
            Category category = new(ID, Title, PersonId);

            foreach (NoteModel note in noteList)
            {
                category.TryAddNote(note.ToLogicModel());
            }

            return category;
        }

        public void AddNote(NoteModel _tag)
        {
            noteList.Add(_tag);
        }
    }
}
