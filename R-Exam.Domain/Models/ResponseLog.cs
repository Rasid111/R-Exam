namespace R_Exam.Domain.Models
{
    public class ResponseLog
    {
        public required string ResponseBody { get; set; }
        public required string ResponseHeaders { get; set; }
        public int StatusCode { get; set; }
        public DateTime EndDateTime { get; set; }
    }
}
