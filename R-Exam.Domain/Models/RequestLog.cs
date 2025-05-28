namespace R_Exam.Domain.Models
{
    public class RequestLog
    {
        public required string RequestId { get; set; }
        public required string Url { get; set; }
        public required string RequestBody { get; set; }
        public required string RequestHeaders { get; set; }
        public required string MethodType { get; set; }
        public string? ClientIp { get; set; }
        public DateTime CreationDateTime { get; set; }
    }
}
