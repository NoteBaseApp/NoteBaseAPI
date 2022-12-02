﻿using NoteBaseDALInterface.Models;

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

            foreach (NoteDTO noteDTO in _categoryDTO.NoteList)
            {
                Note note = new(noteDTO);
                if (IsNoteCompatible(note))
                {
                    TryAddNote(note);
                }
            }
        }

        public CategoryDTO ToDTO()
        {
            CategoryDTO categoryDTO = new(ID, Title, PersonId);

            foreach (Note note in noteList)
            {
                categoryDTO.TryAddNoteDTO(note.ToDTO());
            }

            return categoryDTO;
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
            Response<Note> noteResponse = noteProcessor.GetByCategory(ID);

            foreach (Note note in noteResponse.Data)
            {
                TryAddNote(note);
            }
        }
    }
}