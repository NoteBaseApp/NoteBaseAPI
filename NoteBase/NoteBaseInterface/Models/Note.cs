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
