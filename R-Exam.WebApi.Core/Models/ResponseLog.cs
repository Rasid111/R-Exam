using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace R_Exam.WebApi.Core.Models
{
    public class ResponseLog
    {
        public string ResponseBody { get; set; }
        public string ResponseHeaders { get; set; }
        public int StatusCode { get; set; }
        public DateTime EndDateTime { get; set; }
    }
}
