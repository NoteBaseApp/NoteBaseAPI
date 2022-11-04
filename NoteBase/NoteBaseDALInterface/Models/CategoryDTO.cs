using System.Diagnostics;

namespace NoteBaseDALInterface.Models
{
    public class CategoryDTO
    {
        private readonly List<NoteDTO> noteList = new();

        public int ID { get; }
        public string Title { get; private set; }
        public IReadOnlyList<NoteDTO> NoteList { get { return noteList; } }
        public int PersonId { get; private set; }

        public CategoryDTO(int _id, string _title, int _personId)
        {
            ID = _id;
            Title = _title;
            PersonId = _personId;
        }

        public void TryAddNoteDTO(NoteDTO _noteDTO)
        {
            if (!IsNoteCompatible(_noteDTO))
            {
                throw new Exception("note already in List");
            }

            noteList.Add(_noteDTO);
        }

        public bool IsNoteCompatible(NoteDTO _noteDTO)
        {
            return !noteList.Contains(_noteDTO);
        }
    }
}