using Models.Responses.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Responses
{
    public class NotFoundResponse(string key, string? message = null) : AbstractResponse(message)
    {
        public string Key { get; set; } = key;
    }
}
