using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace R_Exam.Application.Dtos.User
{
    public class UserLoginResponseDto
    {
        public bool Success { get; set; }
        public string? Error { get; set; }
    }
}
