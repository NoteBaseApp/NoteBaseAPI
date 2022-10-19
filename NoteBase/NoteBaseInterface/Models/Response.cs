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
        private readonly List<T> data = new();

        public bool Succeeded { get; set; }
        public int Code { get; set; }
        public string Message { get; set; }
        public IReadOnlyList<T> Data { get { return data; } }

        public Response(bool _succeeded, string _message)
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
