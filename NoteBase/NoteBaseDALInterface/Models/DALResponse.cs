namespace NoteBaseDALInterface.Models
{
    public class DALResponse<T>
    {
        private readonly List<T> data = new();

        public int Status { get; set; }
        public string? Message { get; set; }
        public IEnumerable<T> Data { get { return data; } }

        public DALResponse(int _status, string? _message)
        {
            Status = _status;
            Message = _message;
        }

        public void AddItem(T _Item)
        {
            data.Add(_Item);
        }
    }
}