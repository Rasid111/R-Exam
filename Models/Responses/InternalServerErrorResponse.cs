using Models.Responses.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Responses
{
    public class InternalServerErrorResponse(string? message = null) : AbstractResponse(message) { }
}
