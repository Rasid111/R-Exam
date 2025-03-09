using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace R_Exam.Infrastructre.EntityFramework
{
    public class AccountDbContext : IdentityDbContext<IdentityUser>
    {
        public AccountDbContext(DbContextOptions<AccountDbContext> options) : base(options) { }
    }
}
