using System.Diagnostics;

namespace NoteBaseDALInterface.Models
{
    public class CategoryDTO
    {
        public Guid ID { get; }
        public string Title { get; private set; }
        public Guid PersonId { get; private set; }

        public CategoryDTO(Guid _id, string _title, Guid _personId)
        {
            ID = _id;
            Title = _title;
            PersonId = _personId;
        }
    }
}