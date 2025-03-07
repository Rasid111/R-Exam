namespace R_Exam.Domain.Models
{
    public class ResponseLog
    {
        public string ResponseBody { get; set; }
        public string ResponseHeaders { get; set; }
        public int StatusCode { get; set; }
        public DateTime EndDateTime { get; set; }
    }
}
