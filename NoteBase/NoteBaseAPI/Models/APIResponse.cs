using System.ComponentModel;

namespace NoteBaseAPI.Models
{
    public class APIResponse
    {
        public APIResponseStatus Status { get; set; }
        public string Message { get; set; } = "";
        public Object ResponseBody { get; set; }

        public APIResponse(APIResponseStatus _status) 
        {
            Status = _status;
        }
    }

    public enum APIResponseStatus 
    {
        Success,
        Failure
    }
}
