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

        public Note Create(string _title, string _text, int _categoryId, int _personId)
        {
            //place checks in separate method? or mabye in the Note class
            if (_title == "")
            {
                throw new ArgumentException("Title can't be empty");
            }

            if (_text == "")
            {
                throw new ArgumentException("Text can't be empty");
            }

            if (_categoryId == 0)
            {
                throw new ArgumentException("No valid category was given");
            }

            Note note = GetByTitle(_title);
            if (note.ID != 0)
            {
                throw new Exception("Note With this title already exists");
            }

            //throw exeption when create fails?
            note = new(NoteDAL.Create(_title, _text, _categoryId, _personId));

            //extract method
            List<Tag> tags = ExtractTags(_text);

            foreach (Tag newtag in tags)
            {
                Tag tag = TagProcessor.GetByTitle(newtag.Title);

                if (tag.ID == 0)
                {
                    TagProcessor.Create(newtag.Title);
                    tag = TagProcessor.GetByTitle(newtag.Title);
                }

                NoteDAL.CreateNoteTag(note.ID, tag.ID);
            }
            //

            //get by id or not needed
            return GetByTitle(_title);
        }

        //what if somebody usses a tag with a hashtag in it like #C#
        private List<Tag> ExtractTags(string _text)
        {
            List<Tag> result = new();

            string[] allWords = _text.Split(" ");
            for (int i = 0; i < allWords.Length; i++)
            {
                string word = allWords[i];
                if (word.StartsWith("#"))
                {
                    Tag tag = new(0, word[1..].ToLower());
                    if (!result.Contains(tag))
                    {
                        result.Add(tag);
                    }
                }
            }

            return result;
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

        public Note Update(int _id, string _title, string _text, int _categoryId, int _personId, List<Tag> _tags)
        {
            if (_title == "")
            {
                throw new ArgumentException("Title can't be empty");
            }

            if (_text == "")
            {
                throw new ArgumentException("Text can't be empty");
            }

            if (_categoryId == 0)
            {
                throw new ArgumentException("No valid category was given");
            }

            Note note = GetByTitle(_title);
            if (note.ID != 0 && note.ID != _id)
            {
                throw new Exception("Note With this title already exists");
            }

            //throw exeption when update fails?
            note = new(NoteDAL.Update(_id, _title, _text, _categoryId));
            note.PersonId = _personId;

            if (_tags.Count > 0)
            {
                NoteDAL.DeleteNoteTag(_id);
            }

            List<Tag> newTags = ExtractTags(_text);

            foreach (Tag newtag in newTags)
            {
                Tag tag = TagProcessor.GetByTitle(newtag.Title);

                if (tag.ID == 0)
                {
                    TagProcessor.Create(newtag.Title);
                    tag = TagProcessor.GetByTitle(newtag.Title);
                }

                NoteDAL.CreateNoteTag(note.ID, tag.ID);
            }

            foreach (Tag tag in _tags)
            {
                //get all tags with same title. if there are non delete the tag
                if (newTags.Where(t => t.Title == tag.Title).Count() == 0)
                {
                    TagProcessor.TryDelete(tag.ID, _personId);
                }
            }

            return GetById(_id);
        }

        public void Delete(Note _note, int _PersonId)
        {
            Note note = GetById(_note.ID);
            if (note.ID == 0)
            {
                throw new Exception("Note doesn't exist");
            }

            if (_note.TagList.Count > 0)
            {
                NoteDAL.DeleteNoteTag(_note.ID);
            }

            NoteDAL.Delete(_note.ID);

            foreach (Tag tag in _note.TagList)
            {
                TagProcessor.TryDelete(tag.ID, _PersonId);
            }
        }
    }
}