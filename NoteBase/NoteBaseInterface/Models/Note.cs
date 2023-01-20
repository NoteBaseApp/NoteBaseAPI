using NoteBaseDALInterface.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteBaseLogicInterface.Models
{
    public class Note
    {
        public int ID { get; }
        public string Title { get; private set; }
        public string Text { get; private set; }
        public int CategoryId { get; private set; }
        public int PersonId { get; set; }
        public List<Tag> tagList { get; set; } = new();

        public Note(int _id, string _title, string _text, int _categoryId, int _personId)
        {
            ID = _id;
            Title = _title;
            Text = _text;
            CategoryId = _categoryId;
            PersonId = _personId;
        }

        public Note(NoteDTO _noteDTO)
        {
            ID = _noteDTO.ID;
            Title = _noteDTO.Title;
            Text = _noteDTO.Text;
            CategoryId = _noteDTO.CategoryId;
            PersonId = _noteDTO.PersonId;

            foreach (TagDTO tagDTO in _noteDTO.tagList)
            {
                Tag tag = new(tagDTO);
                tagList.Add(tag);
            }
        }
    }
}
