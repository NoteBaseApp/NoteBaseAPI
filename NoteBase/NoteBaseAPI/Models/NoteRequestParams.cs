namespace UI.Models
{
    public class NoteRequestParams
    {
        public Guid ID { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public Guid CategoryId { get; set; }
        public Guid PersonId { get; set; }
    }
}
