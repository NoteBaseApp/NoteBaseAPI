using NoteBaseDALInterface.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteBaseLogicInterface.Models
{
    public class Note
    {
        private readonly List<Tag> tagList = new();

        public int ID { get; }
        public string Title { get; private set; }
        public string Text { get; private set; }
        public int CategoryId { get; private set; }
        public IReadOnlyList<Tag> TagList { get { return tagList; } }
        public int PersonId { get; set; }

        public Note(int _id, string _title, string _text, int _categoryId)
        {
            ID = _id;
            Title = _title;
            Text = _text;
            CategoryId = _categoryId;
        }

        public Note(NoteDTO _noteDTO)
        {
            ID = _noteDTO.ID;
            Title = _noteDTO.Title;
            Text = _noteDTO.Text;
            CategoryId = _noteDTO.CategoryId;
            PersonId = _noteDTO.PersonId;

            foreach (TagDTO tagDTO in _noteDTO.TagList)
            {
                Tag tag = new Tag(tagDTO);
                if (IsTagCompatible(tag))
                {
                    TryAddTag(tag);
                }
            }
        }

        public NoteDTO ToDTO()
        {
            NoteDTO noteDTO = new NoteDTO(ID, Title, Text, CategoryId);
            noteDTO.PersonId = PersonId;

            foreach (Tag tag in tagList)
            {
                TagDTO tagDTO = tag.ToDTO();

                noteDTO.TryAddTagDTO(tagDTO);
            }

            return noteDTO;
        }

        //what if somebody usses a tag with a hashtag in it like #C#
        //aslo move this to the Note Class
        public void AddTags()
        {
            string[] allWords = Text.Split(" ");
            for (int i = 0; i < allWords.Length; i++)
            {
                string word = allWords[i];
                if (word.StartsWith("#"))
                {
                    Tag tag = new(i, word.Substring(1).ToLower());
                    if (IsTagCompatible(tag))
                    {
                        TryAddTag(tag);
                    }
                }
            }
        }

        public void TryAddTag(Tag _tag)
        {
            if (!IsTagCompatible(_tag))
            {
                throw new Exception("Tag already in list");
            }
            tagList.Add(_tag);
        }

        public bool IsTagCompatible(Tag _tag)
        {
            return !tagList.Contains(_tag);
        }
    }
}
