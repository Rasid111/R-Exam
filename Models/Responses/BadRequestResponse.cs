using Models.Responses.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Responses
{
    public class BadRequestResponse(string? message = null, string? paramName = null) : AbstractResponse(message)
    {
        public string? ParamName { get; set; } = paramName;
    }
}
