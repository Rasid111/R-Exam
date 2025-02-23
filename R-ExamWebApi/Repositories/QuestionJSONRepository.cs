using Microsoft.AspNetCore.Mvc.Formatters;
using Models;
using R_Exam.Repositories.Base;
using R_Exam.Services;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Text.Json;

namespace R_Exam.Repositories
{
    public class QuestionJSONRepository : IQuestionRepository
    {
        static private long id;
        private readonly string path;

        public QuestionJSONRepository(string path = "questions.json")
        {
            this.path = path;
            var questions = GetQuestions();
            id = questions.Count == 0 ? 0 : questions[^1].Id; // Сделать так, чтобы он учитывал и id удаленных вопросов я не смогу. Для этого надо либо создать класс для записи всей информации из JSON файла, либо свой десериализатор писать
        }
        private List<Question> GetQuestions()
        {
            return File.Exists(path) ? JsonSerializer.Deserialize<List<Question>>(File.ReadAllText(path)) ?? [] : [];
        }
        public void Create(Question question)
        {
            var questions = GetQuestions();
            question.Id = ++id;
            questions.Add(question);
            File.WriteAllText(path, JsonSerializer.Serialize(questions));
        }
        public Question? Get(int id)
        {
            var questions = GetQuestions();
            return questions.FirstOrDefault(question => (question.Id == id));
        }
        public bool Update(Question questionData)
        {
            var questions = GetQuestions();
            var index = questions.FindIndex(question => question.Id == questionData.Id);
            if (index == -1)
                return false;
            questions[index] = questionData;
            File.WriteAllText(path, JsonSerializer.Serialize(questions));
            return true;
        }
        public bool Delete(int id)
        {
            var questions = GetQuestions();
            var question = questions.Find((question) => question.Id == id);
            if (question == null)
                return false;
            var resultStatus = questions.Remove(question);
            if (resultStatus)
                File.WriteAllText(path, JsonSerializer.Serialize(questions));
            return resultStatus;
        }
    }
}
