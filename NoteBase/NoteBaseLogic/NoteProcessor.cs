using NoteBaseDALInterface;
using NoteBaseDALInterface.Models;
using NoteBaseInterface;
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
            return GetByTitle(_title).ID == 0;
        }

        public bool IsTitleUnique(string _title, int _id)
        {
            return GetByTitle(_title).ID == 0 || GetByTitle(_title).ID == _id;
        }

        public bool DoesNoteExits(int _id)
        {
            return _id != 0 && GetById(_id).ID != 0;
        }

        public Note Create(string _title, string _text, int _categoryId, int _personId)
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
            if (_categoryId == 0)
            {
                throw new ArgumentException("No valid category was given");
            }

            //throw exeption when create fails?
            Note note = new(NoteDAL.Create(_title, _text, _categoryId, _personId));

            TagProcessor.CreateTags(_text, note.ID);

            //get by id or not needed
            return GetByTitle(_title);
        }

        public Note GetById(int _noteId)
        {
            NoteDTO noteDTO = NoteDAL.GetById(_noteId);

            return new(noteDTO);
        }

        public List<Note> GetByPerson(int _personId)
        {
            List<Note> noteList = new();

            List<NoteDTO> noteDTOs = NoteDAL.GetByPerson(_personId);

            foreach (NoteDTO noteDTO in noteDTOs)
            {
                noteList.Add(new(noteDTO));
            }

            return noteList;
        }


        public Note GetByTitle(string _Title)
        {
            NoteDTO noteDTO = NoteDAL.GetByTitle(_Title);

            return new(noteDTO);
        }

        public List<Note> GetByCategory(int _catId)
        {
            List<Note> noteList = new();

            List<NoteDTO> noteDTOs = NoteDAL.GetByCategory(_catId);
            foreach (NoteDTO noteDTO in noteDTOs)
            {
                noteList.Add(new(noteDTO));
            }

            return noteList;
        }

        public List<Note> GetByTag(int _tagId)
        {
            List<Note> noteList = new();

            List<NoteDTO> noteDTOs = NoteDAL.GetByTag(_tagId);
            foreach (NoteDTO noteDTO in noteDTOs)
            {
                noteList.Add(new(noteDTO));
            }

            return noteList;
        }

        public Note Update(int _id, string _title, string _text, int _categoryId, int _personId, List<Tag> _tags)
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
            if (_categoryId == 0)
            {
                throw new ArgumentException("No valid category was given");
            }

            //throw exeption when update fails?
            Note note = new(NoteDAL.Update(_id, _title, _text, _categoryId));
            note.PersonId = _personId;
            TagProcessor.UpdateTags(_id, _text, _personId, _tags);

            return GetById(_id);
        }

        public void Delete(int _noteId, List<Tag> _tagList, int _PersonId)
        {
            Note note = GetById(_noteId);
            if (note.ID == 0)
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
                TagProcessor.DeleteWhenUnused(tag.ID, _PersonId);
            }
            //
        }
    }
}