namespace App.Models
{
    public class ResponseModel<T>
    {
        private readonly List<T> data = new();

        public bool Succeeded { get; set; }
        public int Code { get; set; }
        public string Message { get; set; }
        public IReadOnlyList<T> Data { get { return data; } }

        public ResponseModel(bool _succeeded)
        {
            Succeeded = _succeeded;
            Message = "";
        }

        public void AddItem(T _Item)
        {
            data.Add(_Item);
        }
    }
}
