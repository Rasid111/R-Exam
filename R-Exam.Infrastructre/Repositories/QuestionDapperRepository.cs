using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using R_Exam.Domain.Models;
using R_Exam.Domain.Repositories;

namespace R_Exam.Infrastructre.Repositories
{
    public class QuestionDapperRepository(string connectionString) : IQuestionRepository
    {
        private readonly string connectionString = connectionString;

        public async Task<List<Question>> Get()
        {
            using IDbConnection db = new SqlConnection(connectionString);
            List<Question> questions = (await db.QueryAsync<Question>("SELECT * FROM Questions")).ToList();
            foreach (Question question in questions)
            {
                question.Answers = (await db.QueryAsync<Answer>("SELECT * FROM Answers WHERE QuestionId = @Id", new { id = question.Id })).ToList();
            }
            return questions;
        }
        public async Task<int> Create(Question question)
        {
            using IDbConnection db = new SqlConnection(connectionString);
            var sqlQuery = @"INSERT INTO Questions (Title, CorrectAnswerTitle) 
                        VALUES(@Title, @CorrectAnswerTitle); 
                        select scope_identity() as int;";
            int questionId = (await db.QueryAsync<int>(sqlQuery, question)).First();
            sqlQuery = @"INSERT INTO Answers (Title, QuestionId) 
                    VALUES(@Title, @QuestionId)";
            question.Answers.ForEach(answer => answer.QuestionId = questionId);
            await db.ExecuteAsync(sqlQuery, question.Answers);
            return questionId;
        }
        public async Task<Question?> Get(int id)
        {
            using IDbConnection db = new SqlConnection(connectionString);
            Question? question = (await db.QueryAsync<Question>("SELECT * FROM Questions WHERE Id = @Id", new { id })).FirstOrDefault();
            if (question != null)
                question.Answers = (await db.QueryAsync<Answer>("SELECT * FROM Answers WHERE QuestionId = @Id", new { id })).ToList();
            return question;
        }
        public async Task<bool> Update(Question questionData)
        {
            using IDbConnection db = new SqlConnection(connectionString);
            var sqlQuery = "UPDATE Questions SET Title = @Title, CorrectAnswerTitle = @CorrectAnswerTitle WHERE Id = @Id";
            var affectedRowsCount = await db.ExecuteAsync(sqlQuery, questionData);

            if (affectedRowsCount == 0)
                return false;

            sqlQuery = @"DELETE Answers Where QuestionId = @Id;";
            await db.ExecuteAsync(sqlQuery, questionData);

            sqlQuery = @"INSERT INTO Answers (Title, QuestionId) 
                        VALUES(@Title, @QuestionId)";
            questionData.Answers.ForEach(answer => answer.QuestionId = questionData.Id);
            await db.ExecuteAsync(sqlQuery, questionData.Answers);

            return true;
        }
        public async Task<bool> Delete(int id)
        {
            using IDbConnection db = new SqlConnection(connectionString);
            return await db.ExecuteAsync(@"DELETE FROM Questions
                                WHERE Id = @Id", new { id }) > 0;
        }
    }
}
