using Models;
using R_Exam.Repositories.Base;
using System.Data;
using System.Text.Json;
using Microsoft.Data.SqlClient;
using Dapper;
using R_Exam.Exceptions;


namespace R_Exam.Repositories
{
    public class QuestionDapperRepository : IQuestionRepository
    {
        private readonly string connectionString;

        public QuestionDapperRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }
        private List<Question> GetQuestions()
        {
            using IDbConnection db = new SqlConnection(connectionString);
            return db.Query<Question>("SELECT * FROM Questions").ToList();
        }
        public void CreateQuestion(Question question)
        {
            if (question.Answers.Find((answer) => answer.Title == question.CorrectAnswerTitle) == null)
                throw new ArgumentException("Question must have a correct answer", nameof(question.CorrectAnswerTitle));

            using IDbConnection db = new SqlConnection(connectionString);
            var sqlQuery = @"INSERT INTO Questions (Title, CorrectAnswerTitle) 
                            VALUES(@Title, @CorrectAnswerTitle); 
                            select scope_identity() as int;";
            int questionId = db.Query<int>(sqlQuery, question).First();
            sqlQuery = @"INSERT INTO Answers (Title, QuestionId) 
                        VALUES(@Title, @QuestionId)";
            question.Answers.ForEach(answer => answer.QuestionId = questionId);
            db.Execute(sqlQuery, question.Answers);
        }
        public Question? GetQuestion(int id)
        {
            using IDbConnection db = new SqlConnection(connectionString);
            Question? question = db.Query<Question>("SELECT * FROM Questions WHERE Id = @Id", new { id }).FirstOrDefault();
            if (question != null)
                question.Answers = db.Query<Answer>("SELECT * FROM Answers WHERE QuestionId = @Id", new { id }).ToList();
            return question;
        }
        public bool UpdateQuestion(Question questionData)
        {
            if (questionData.Answers.Find((answer) => answer.Title == questionData.CorrectAnswerTitle) == null)
                throw new ArgumentException("Question must have a correct answer", nameof(questionData.CorrectAnswerTitle));

            using IDbConnection db = new SqlConnection(connectionString);
            var sqlQuery = "UPDATE Questions SET Title = @Title, CorrectAnswerTitle = @CorrectAnswerTitle WHERE Id = @Id";
            var affectedRowsCount = db.Execute(sqlQuery, questionData);

            if (affectedRowsCount == 0)
                return false;

            sqlQuery = @"DELETE Answers Where QuestionId = @Id;";
            db.Execute(sqlQuery, questionData);

            sqlQuery = @"INSERT INTO Answers (Title, QuestionId) 
                        VALUES(@Title, @QuestionId)";
            questionData.Answers.ForEach(answer => answer.QuestionId = questionData.Id);
            db.Execute(sqlQuery, questionData.Answers);

            return true;
        }
        public bool DeleteQuestion(int id)
        {

            using IDbConnection db = new SqlConnection(connectionString);
            return (db.Execute(@"DELETE FROM Questions
                                WHERE Id = @Id", new { id }) > 0);
        }
    }
}
