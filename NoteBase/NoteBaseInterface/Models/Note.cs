using NoteBaseDALInterface.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteBaseLogicInterface.Models
{
    public class Note: IModel
    {
        private readonly List<Tag> tagList = new();

        public int ID { get; }
        public string Title { get; private set; }
        public string MainBody { get; private set; }
        public Category Category { get; private set; }
        public IReadOnlyList<Tag> TagList { get { return tagList; } }
        public string UserMail { get; set; }

        public Note(int _id, string _title, string _mainBody, Category _category)
        {
            ID = _id;
            Title = _title;
            MainBody = _mainBody;
            Category = _category;
        }

        public NoteDTO ToDTO()
        {
            CategoryDTO catDTO = new(Category.ID, Category.Title);

            NoteDTO noteDTO = new NoteDTO(ID, Title, MainBody, catDTO);
            noteDTO.UserMail = UserMail;

            foreach (Tag tag in tagList)
            {
                TagDTO tagDTO = tag.ToDTO();

                noteDTO.TryAddTagDTO(tagDTO);
            }

            return noteDTO;
        }

        public void TryAddTag(Tag _tag)
        {
            if (tagList.Contains(_tag))
            {
                throw new Exception("Tag already in list");
            }
            tagList.Add(_tag);
        }
    }
}
