using NoteBaseDALInterface.Models;

namespace NoteBaseLogicInterface.Models
{
    public class Category
    {
        private readonly List<Note> noteList = new();

        public int ID { get; }
        public string Title { get; private set; }
        public IReadOnlyList<Note> NoteList { get { return noteList; } }
        public int PersonId { get; private set; }

        public Category(int _id, string _title, int _personId)
        {
            ID = _id;
            Title = _title;
            PersonId = _personId;
        }

        public Category(CategoryDTO _categoryDTO)
        {
            ID = _categoryDTO.ID;
            Title = _categoryDTO.Title;
            PersonId = _categoryDTO.PersonId;
        }

        public void TryAddNote(Note _note)
        {
            if (!IsNoteCompatible(_note))
            {
                throw new Exception("note already in List");
            }

            noteList.Add(_note);
        }

        public bool IsNoteCompatible(Note _note)
        {
            return !noteList.Contains(_note);
        }

        public void FillNoteList(INoteProcessor noteProcessor)
        {
            List<Note> notes = noteProcessor.GetByCategory(ID);

            foreach (Note note in notes)
            {
                TryAddNote(note);
            }
        }
    }
}