using NoteBaseDALInterface.Models;
using System.Diagnostics;

namespace NoteBaseLogicInterface.Models
{
    public class Category
    {
        private readonly List<Note> noteList = new();

        public int ID { get; }
        public string Title { get; private set; }
        public int PersonId { get; private set; }
        public IReadOnlyList<Note> NoteList { get { return noteList; } }

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

        public void TryAddNote(Note _note)
        {
            if (!IsNoteCompatible(_note))
            {
                throw new Exception("Note already in list");
            }
            noteList.Add(_note);
        }

        public bool IsNoteCompatible(Note _note)
        {
            return !noteList.Contains(_note);
        }
    }
}