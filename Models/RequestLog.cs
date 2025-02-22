using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class RequestLog
    {
        public string RequestId { get; set; }
        public string Url { get; set; }
        public string RequestBody { get; set; }
        public string RequestHeaders { get; set; }
        public string MethodType { get; set; }
        public string ClientIp { get; set; }
        public DateTime CreationDateTime { get; set; }
    }
}
