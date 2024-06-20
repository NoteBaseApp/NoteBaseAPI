namespace UI.Models
{
    public class NoteRequestParams
    {
        public Guid ID { get; set; } = new Guid("00000000-0000-0000-0000-000000000000");
        public string Title { get; set; } = "";
        public string Text { get; set; } = "";
        public Guid CategoryId { get; set; }
    }
}
