using NoteBaseDALInterface;
using NoteBaseDALInterface.Models;
using NoteBaseLogicInterface;
using NoteBaseLogicInterface.Models;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;

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

        public bool IsValidTitle(string _title)
        {
            //needs work (entering just spaces should not be seen as valid)
            return _title != "";
        }

        public bool IsValidText(string _title)
        {
            //needs work (entering just spaces should not be seen as valid)
            return _title != "";
        }

        public bool IsTitleUnique(string _title)
        {
            return GetByTitle(_title).ID == Guid.Parse("00000000-0000-0000-0000-000000000000");
        }

        public bool IsTitleUnique(string _title, Guid _id)
        {
            return GetByTitle(_title).ID == Guid.Parse("00000000-0000-0000-0000-000000000000") || GetByTitle(_title).ID == _id;
        }

        public bool DoesNoteExits(Guid _id)
        {
            return _id != Guid.Parse("00000000-0000-0000-0000-000000000000") && GetById(_id).ID != Guid.Parse("00000000-0000-0000-0000-000000000000");
        }

        public Note Create(string _title, string _text, Guid _categoryId, Guid _personId)
        {
            if (!IsValidTitle(_title))
            {
                throw new ArgumentException("Title can't be empty");
            }

            if (!IsTitleUnique(_title))
            {
                throw new Exception("Note With this title already exists");
            }

            if (!IsValidText(_text))
            {
                throw new ArgumentException("Text can't be empty");
            }

            //how to check this. check exists in categoryProcessor
            if (_categoryId == Guid.Parse("00000000-0000-0000-0000-000000000000"))
            {
                throw new ArgumentException("No valid category was given");
            }

            NoteDTO noteDTO = NoteDAL.Create(_title, _text, _categoryId, _personId);
            Note note = new(noteDTO.ID, noteDTO.Title, noteDTO.Text, noteDTO.CategoryId, noteDTO.PersonId);

            TagProcessor.CreateTags(_text, note.ID);

            //get by id or not needed
            return GetByTitle(_title);
        }

        public Note GetById(Guid _noteId)
        {
            NoteDTO noteDTO = NoteDAL.GetById(_noteId);
            Note note = new(noteDTO.ID, noteDTO.Title, noteDTO.Text, noteDTO.CategoryId, noteDTO.PersonId);
            foreach (TagDTO item in noteDTO.tagList)
            {
                note.tagList.Add(new(item.ID, item.Title));
            }

            return note;
        }

        public List<Note> GetByPerson(Guid _personId)
        {
            List<Note> noteList = new();

            List<NoteDTO> noteDTOs = NoteDAL.GetByPerson(_personId);

            foreach (NoteDTO noteDTO in noteDTOs)
            {
                Note note = new(noteDTO.ID, noteDTO.Title, noteDTO.Text, noteDTO.CategoryId, noteDTO.PersonId);
                foreach (TagDTO item in noteDTO.tagList)
                {
                    note.tagList.Add(new(item.ID, item.Title));
                }
                noteList.Add(note);
            }

            return noteList;
        }


        public Note GetByTitle(string _Title)
        {
            NoteDTO noteDTO = NoteDAL.GetByTitle(_Title);

            Note note = new(noteDTO.ID, noteDTO.Title, noteDTO.Text, noteDTO.CategoryId, noteDTO.PersonId);
            foreach (TagDTO item in noteDTO.tagList)
            {
                note.tagList.Add(new(item.ID, item.Title));
            }

            return note;
        }

        public List<Note> GetByCategory(Guid _catId)
        {
            List<Note> noteList = new();

            List<NoteDTO> noteDTOs = NoteDAL.GetByCategory(_catId);
            foreach (NoteDTO noteDTO in noteDTOs)
            {
                Note note = new(noteDTO.ID, noteDTO.Title, noteDTO.Text, noteDTO.CategoryId, noteDTO.PersonId);
                foreach (TagDTO item in noteDTO.tagList)
                {
                    note.tagList.Add(new(item.ID, item.Title));
                }
                noteList.Add(note);
            }

            return noteList;
        }

        public List<Note> GetByTag(Guid _tagId, Guid _personId)
        {
            List<Note> noteList = new();

            List<NoteDTO> noteDTOs = NoteDAL.GetByTag(_tagId);
            foreach (NoteDTO noteDTO in noteDTOs)
            {
                if (noteDTO.PersonId == _personId)
                {
                    Note note = new(noteDTO.ID, noteDTO.Title, noteDTO.Text, noteDTO.CategoryId, noteDTO.PersonId);
                    foreach (TagDTO item in noteDTO.tagList)
                    {
                        note.tagList.Add(new(item.ID, item.Title));
                    }
                    noteList.Add(note);
                }
            }

            return noteList;
        }

        public Note Update(Guid _id, string _title, string _text, Guid _categoryId, Guid _personId, List<Tag> _tags)
        {
            if (!IsValidTitle(_title))
            {
                throw new ArgumentException("Title can't be empty");
            }

            if (!IsTitleUnique(_title, _id))
            {
                throw new Exception("Note With this title already exists");
            }

            if (!IsValidText(_text))
            {
                throw new ArgumentException("Text can't be empty");
            }

            //how to check this. check exists in categoryProcessor
            if (_categoryId == Guid.Parse("00000000-0000-0000-0000-000000000000"))
            {
                throw new ArgumentException("No valid category was given");
            }

            NoteDTO noteDTO = NoteDAL.Update(_id, _title, _text, _categoryId);
            Note note = new(noteDTO.ID, noteDTO.Title, noteDTO.Text, noteDTO.CategoryId, noteDTO.PersonId);

            TagProcessor.UpdateTags(_id, _text, _personId, _tags);

            return GetById(_id);
        }

        public void Delete(Guid _noteId, List<Tag> _tagList, Guid _PersonId)
        {
            Note note = GetById(_noteId);
            if (note.ID == Guid.Parse("00000000-0000-0000-0000-000000000000"))
            {
                throw new Exception("Note doesn't exist");
            }

            //should this be in the tagprocessor?
            if (_tagList.Count > 0)
            {
                TagProcessor.DeleteNoteTag(_noteId);
            }

            NoteDAL.Delete(_noteId);

            foreach (Tag tag in _tagList)
            {
                TagProcessor.DeleteWhenUnused(tag.ID);
            }
            //
        }
    }
}