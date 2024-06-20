using System.ComponentModel;

namespace NoteBaseAPI.Models
{
    public class CategoryRequestParams
    {
        public Guid ID { get; set; } = new Guid("00000000-0000-0000-0000-000000000000");
        public string Title { get; set; } = "";
    }
}
