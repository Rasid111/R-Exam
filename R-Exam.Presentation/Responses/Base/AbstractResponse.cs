﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace R_Exam.WebApi.Presentation.Responses.Base
{
    public abstract class AbstractResponse(string? Message = null)
    {
        public string? Message { get; set; } = Message;
    }
}
