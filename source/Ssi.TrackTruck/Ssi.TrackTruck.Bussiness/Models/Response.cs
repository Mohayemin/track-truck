namespace Ssi.TrackTruck.Bussiness.Models
{
    public class Response
    {
        public bool IsError { get; set; }
        public string Status { get; set; }
        public string Message { get; set; }

        public object Data { get; set; }

        public static Response Error(string status, string message = null, object data = null)
        {
            return new Response { IsError = true, Status = status, Message = message, Data = data };
        }

        public static Response Success(object data = null, string message = null, string status = null)
        {
            return new Response { IsError = false, Status = status, Message = message, Data = data };
        }
    }
}
