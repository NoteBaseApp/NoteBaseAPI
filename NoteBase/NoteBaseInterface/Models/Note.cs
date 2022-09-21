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
        public string MainBody { get; private set; }
        public Category Category { get; private set; }
        public IEnumerable<Tag> TagList { get { return tagList; } }

        public Note(int _id, string _title, string _mainBody, Category _category)
        {
            ID = _id;
            Title = _title;
            MainBody = _mainBody;
            Category = _category;
        }
    }
}
