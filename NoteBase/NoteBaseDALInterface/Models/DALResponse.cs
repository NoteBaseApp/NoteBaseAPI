namespace NoteBaseDALInterface.Models
{
    public class DALResponse<T>
    {
        private readonly List<T> data = new();

        public bool Succeeded { get; set; }
        public int Code { get; set; }
        public string Message { get; set; }
        public IReadOnlyList<T> Data { get { return data; } }

        public DALResponse(bool _succeeded, string _message)
        {
            Succeeded = _succeeded;
            Message = _message;
        }

        public void AddItem(T _Item)
        {
            data.Add(_Item);
        }
    }
}