using Microsoft.EntityFrameworkCore;
using R_Exam.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace R_Exam.Infrastructre.EntityFramework
{
    public class R_ExamDbContext(DbContextOptions<R_ExamDbContext> options) : DbContext(options)
    {
        public DbSet<Question> Questions { get; set; }
        public DbSet<Answer> Answers { get; set; }
    }
}
