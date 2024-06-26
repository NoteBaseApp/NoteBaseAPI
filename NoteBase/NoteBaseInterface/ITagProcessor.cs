﻿using NoteBaseDALInterface.Models;
using NoteBaseLogicInterface.Models;

namespace NoteBaseLogicInterface
{
    public interface ITagProcessor
    {
        void CreateTags(string _text, Guid NoteId);
        void UpdateTags(Guid _noteId, string _text, Guid _personId, List<Tag> _tags);
        bool IsValidTitle(string _title);
        bool DoesTagExits(Guid _id);
        Tag Create(string _title);
        Tag GetById(Guid _tagId);
        List<Tag> GetByPerson(Guid _PersonId);
        Tag GetByTitle(string _Title);
        void DeleteWhenUnused(Guid _tagId);
        void DeleteNoteTag(Guid _noteId);
    }
}