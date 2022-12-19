using NoteBaseDALInterface;
using NoteBaseDALInterface.Models;
using NoteBaseInterface;
using NoteBaseLogicInterface;
using NoteBaseLogicInterface.Models;

namespace NoteBaseLogic
{
    public class NoteProcessor : INoteProcessor
    {
        private readonly INoteDAL NoteDAL;
        private readonly ITagProcessor TagProcessor;
        public NoteProcessor(INoteDAL _noteDAL, ITagProcessor _tagProcessor)
        {
            NoteDAL = _noteDAL;
            TagProcessor = _tagProcessor;
        }

        public Note Create(Note _note)
        {
            if (_note.Title == "")
            {
                throw new ArgumentException("Title can't be empty");
            }

            if (_note.Text == "")
            {
                throw new ArgumentException("Text can't be empty");
            }

            if (_note.CategoryId == 0)
            {
                throw new ArgumentException("No valid category was given");
            }

            Note note = GetByTitle(_note.Title);
            if (note.ID != 0)
            {
                throw new Exception("Note With this title already exists");
            }

            NoteDAL.Create(_note.ToDTO());

            _note.AddTags();

            note = GetByTitle(_note.Title);

            foreach (Tag newtag in _note.TagList)
            {
                Tag tag = TagProcessor.GetByTitle(newtag.Title);

                if (tag.ID == 0)
                {
                    TagProcessor.Create(newtag);
                    tag = TagProcessor.GetByTitle(newtag.Title);
                }

                NoteDAL.CreateNoteTag(note.ID, tag.ID);
            }

            return note;
        }

        public Note GetById(int _noteId)
        {
            NoteDTO noteDTO = NoteDAL.GetById(_noteId);

            return new(noteDTO);
        }

        public List<Note> GetByPerson(int _personId)
        {
            List<Note> result = new();

            List<NoteDTO> noteDTOs = NoteDAL.GetByPerson(_personId);

            foreach (NoteDTO noteDTO in noteDTOs)
            {
                result.Add(new(noteDTO));
            }

            return result;
        }


        public Note GetByTitle(string _Title)
        {
            NoteDTO noteDTO = NoteDAL.GetByTitle(_Title);

            return new(noteDTO);
        }

        public List<Note> GetByCategory(int _catId)
        {
            List<Note> result = new();

            List<NoteDTO> noteDTOs = NoteDAL.GetByCategory(_catId);
            foreach (NoteDTO noteDTO in noteDTOs)
            {
                result.Add(new(noteDTO));
            }

            return result;
        }

        public List<Note> GetByTag(int _tagId)
        {
            List<Note> result = new();

            List<NoteDTO> noteDTOs = NoteDAL.GetByTag(_tagId);
            foreach (NoteDTO noteDTO in noteDTOs)
            {
                result.Add(new(noteDTO));
            }

            return result;
        }

        public Note Update(Note _note)
        {
            if (_note.Title == "")
            {
                throw new ArgumentException("Title can't be empty");
            }

            if (_note.Text == "")
            {
                throw new ArgumentException("Text can't be empty");
            }

            if (_note.CategoryId == 0)
            {
                throw new ArgumentException("No valid category was given");
            }

            Note note = GetByTitle(_note.Title);
            if (note.ID != 0)
            {
                throw new Exception("Note With this title already exists");
            }

            int noteUpdateResponse = NoteDAL.Update(_note.ToDTO());

            _note.AddTags();

            NoteDAL.DeleteNoteTag(_note.ID);

            foreach (Tag newTag in _note.TagList)
            {
                Tag tag = TagProcessor.GetByTitle(newTag.Title);

                if (tag.ID == 0)
                {
                    TagProcessor.Create(newTag);
                    tag = TagProcessor.GetByTitle(newTag.Title);
                }

                //update notetag table
                NoteDAL.CreateNoteTag(_note.ID, tag.ID);
            }

            foreach (Tag tag in _note.TagList)
            {
                TagProcessor.TryDelete(tag.ID, _note.PersonId);
            }

            return GetById(_note.ID);
        }

        public int Delete(Note _note, int _PersonId)
        {
            Note note = GetById(_note.ID);
            if (note.ID == 0)
            {
                throw new Exception("Note doesn't exist");
            }


            int noteTagDeleteReponse = NoteDAL.DeleteNoteTag(_note.ID);
            if (noteTagDeleteReponse == 0)
            {
                throw new Exception("Could not Delete NoteTag");
            }

            int noteDeleteReponse = NoteDAL.Delete(_note.ID);

            foreach (Tag tag in _note.TagList)
            {
                TagProcessor.TryDelete(tag.ID, _PersonId);
            }

            return noteDeleteReponse;
        }
    }
}