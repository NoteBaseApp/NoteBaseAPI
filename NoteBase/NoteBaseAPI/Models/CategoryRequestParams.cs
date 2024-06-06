using System.ComponentModel;

namespace NoteBaseAPI.Models
{
    public class CategoryRequestParams
    {
        public Guid ID { get; set; }
        public string Title { get; set; }
        public Guid PersonId { get; set; }
    }
}
