namespace UI.Models
{
    public class NoteRequestParams
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public int CategoryId { get; set; }
        public int PersonId { get; set; }
    }
}
