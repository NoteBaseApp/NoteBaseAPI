﻿using NoteBaseDALInterface.Models;
using NoteBaseDALInterface;
using NoteBaseLogicInterface.Models;
using NoteBaseLogicInterface;

namespace NoteBaseLogic
{
    public class TagProcessor : ITagProcessor
    {
        private readonly ITagDAL TagDAL;
        private readonly INoteDAL NoteDAL;

        public TagProcessor(ITagDAL _tagDAL, INoteDAL _noteDAL)
        {
            TagDAL = _tagDAL;
            NoteDAL = _noteDAL;
        }

        public bool IsValidTitle(string _title)
        {
            //needs work (entering just spaces should not be seen as valid)
            return _title != "";
        }

        public bool DoesTagExits(Guid _id)
        {
            return _id != Guid.Parse("00000000-0000-0000-0000-000000000000") && GetById(_id).ID != Guid.Parse("00000000-0000-0000-0000-000000000000");
        }

        //rename
        public void CreateTags(string _text, Guid NoteId)
        {
            List<Tag> tags = ExtractTags(_text);

            foreach (Tag newtag in tags)
            {
                Tag tag = GetByTitle(newtag.Title);

                if (tag.ID == Guid.Parse("00000000-0000-0000-0000-000000000000"))
                {
                    Create(newtag.Title);
                    tag = GetByTitle(newtag.Title);
                }

                TagDAL.CreateNoteTag(NoteId, tag.ID);
            }
        }

        //rename
        public void UpdateTags(Guid _noteId, string _text, Guid _personId, List<Tag> _tags)
        {
            if (_tags.Count > 0)
            {
                TagDAL.DeleteNoteTag(_noteId);
            }

            List<Tag> newTags = ExtractTags(_text);

            foreach (Tag newtag in newTags)
            {
                Tag tag = GetByTitle(newtag.Title);

                if (tag.ID == Guid.Parse("00000000-0000-0000-0000-000000000000"))
                {
                    Create(newtag.Title);
                    tag = GetByTitle(newtag.Title);
                }

                TagDAL.CreateNoteTag(_noteId, tag.ID);
            }

            foreach (Tag tag in _tags)
            {
                //get all tags with same title. if there are non delete the tag
                if (newTags.Where(t => t.Title == tag.Title).Count() == 0)
                {
                    DeleteWhenUnused(tag.ID);
                }
            }
        }

        //what if somebody usses a tag with a hashtag in it like #C#
        private List<Tag> ExtractTags(string _text)
        {
            List<Tag> NewTagList = new();

            string[] allWords = _text.Split(" ");
            for (int i = 0; i < allWords.Length; i++)
            {
                string word = allWords[i];
                if (word.StartsWith("#"))
                {
                    Tag tag = new(Guid.Parse("00000000-0000-0000-0000-000000000000"), word[1..].ToLower());
                    if (!NewTagList.Contains(tag))
                    {
                        NewTagList.Add(tag);
                    }
                }
            }

            return NewTagList;
        }

        public Tag Create(string _title)
        {
            if (!IsValidTitle(_title))
            {
                throw new ArgumentException("Title can't be empty");
            }

            TagDTO tagDTO = TagDAL.Create(_title);
            return new(tagDTO.ID, tagDTO.Title);
        }

        public Tag GetById(Guid _tagId)
        {
            TagDTO tagDTO = TagDAL.GetById(_tagId);

            return new(tagDTO.ID, tagDTO.Title);
        }

        //need to remake this for using person id
        public List<Tag> GetByPerson(Guid _personId)
        {
            List<Tag> tagList = new();

            List<TagDTO> tagDTOs = TagDAL.GetByPerson(_personId);
            foreach (TagDTO tagDTO in tagDTOs)
            {
                tagList.Add(new(tagDTO.ID, tagDTO.Title));
            }

            return tagList;
        }

        public Tag GetByTitle(string _title)
        {
            TagDTO tagDTO = TagDAL.GetByTitle(_title);

            return new(tagDTO.ID, tagDTO.Title);
        }

        public void DeleteWhenUnused(Guid _tagId)
        {
            //get all notes that use this tag
            List<NoteDTO> tagList = NoteDAL.GetByTag(_tagId);

            //is tag in use? return
            if (tagList.Count > 0)
            {
                return;
            }

            //else delete it
            TagDAL.Delete(_tagId);
        }

        public void DeleteNoteTag(Guid _noteId)
        {
            TagDAL.DeleteNoteTag(_noteId);
        }
    }
}
