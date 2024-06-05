using System.ComponentModel;

namespace NoteBaseAPI.Models
{
    public class CategoryRequestParams
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public int PersonId { get; set; }
    }
}
