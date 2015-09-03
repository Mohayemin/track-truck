namespace Ssi.TrackTruck.Bussiness.Models
{
    public class Response
    {
        public bool IsError { get; set; }
        public string Status { get; set; }
        public string Message { get; set; }

        public static Response Error(string status, string message = null)
        {
            return new Response { IsError = true, Status = status, Message = message };
        }

        public static Response Success(string status = null, string message = null)
        {
            return new Response { IsError = false, Status = status, Message = message };
        }
    }
}
