using Microsoft.EntityFrameworkCore;
using R_Exam.Domain.Models;
using R_Exam.Infrastructre.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace R_Exam.Infrastructre.Repositories
{
    public class QuestionEntityFrameWorkRepository(R_ExamDbContext dbContext) : IQuestionRepository
    {
        public readonly R_ExamDbContext dbContext = dbContext;

        public Task<int> Create(Question question)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Question>> Get()
        {
            var questions = await dbContext.Questions
                .ToListAsync();
            return questions;
        }

        public async Task<Question?> Get(int id)
        {
            var question = await dbContext.Questions.
                Include(dbContext => dbContext.Answers)
                .FirstOrDefaultAsync(question => question.Id == id);
            return question;
        }

        public Task<bool> Update(Question questionData)
        {
            throw new NotImplementedException();
        }
    }
}
