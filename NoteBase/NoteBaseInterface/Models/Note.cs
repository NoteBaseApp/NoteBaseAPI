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
        public Guid ID { get; }
        public string Title { get; private set; }
        public string Text { get; private set; }
        public Guid CategoryId { get; private set; }
        public Guid PersonId { get; set; }
        public List<Tag> tagList { get; set; } = new();

        public Note(Guid _id, string _title, string _text, Guid _categoryId, Guid _personId)
        {
            ID = _id;
            Title = _title;
            Text = _text;
            CategoryId = _categoryId;
            PersonId = _personId;
        }
    }
}
