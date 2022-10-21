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
        public Category Category { get; private set; }
        public IReadOnlyList<Tag> TagList { get { return tagList; } }
        public int PersonId { get; set; }

        public Note(int _id, string _title, string _text, Category _category)
        {
            ID = _id;
            Title = _title;
            Text = _text;
            Category = _category;
        }

        public NoteDTO ToDTO()
        {
            CategoryDTO catDTO = new(Category.ID, Category.Title, Category.PersonId);

            NoteDTO noteDTO = new NoteDTO(ID, Title, Text, catDTO);
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
