using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace R_Exam.Application.Dtos.User
{
    public class UserLoginRequestDto
    {
        public required string Email { get; set; }
        public required string Password { get; set; }
    }
}
