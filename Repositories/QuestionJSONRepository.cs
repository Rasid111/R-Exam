using R_Exam.Models;
using R_Exam.Repositories.Base;
using R_Exam.Services;

namespace R_Exam.Repositories
{
    public class QuestionJSONRepository : IQuestionRepository
    {
        private List<Question> questions = [];
        public void CreateQuestion(Question question)
        {
            questions.Add(question);
        }
        public Question GetQuestion(int id)
        {
            var question = questions.First(question => (question.Id == id));
            return question;
        }
        public void UpdateQuestion(Question questionData)
        {
            var index = questions.FindIndex(question => question.Id == questionData.Id);
            questions[index] = questionData;
        }
        public void DeleteQuestion(int id)
        {
            questions.Remove(GetQuestion(id));
        }
    }
}
