namespace Company.Api.Models
{
    public class ErrorResponse
    {
        public string Message { get; set; }
        public string Detail { get; set; }
        public string StackTrace { get; set; }
        public int StatusCode { get; set; }
    }
}
