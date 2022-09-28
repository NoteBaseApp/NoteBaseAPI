using NoteBaseDALInterface.Models;
using NoteBaseLogicInterface.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteBaseLogicInterface.Models
{
    public class Response<T>
    {
        private readonly List<IModel<T>> data = new();

        public int Status { get; set; }
        public string? Message { get; set; }
        public IReadOnlyList<IModel<T>> Data { get { return data; } }

        public Response(int _status, string? _message)
        {
            Status = _status;
            Message = _message;
        }

        public void AddItem(IModel<T> _Item)
        {
            data.Add(_Item);
        }
    }
}
