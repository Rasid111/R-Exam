namespace R_Exam.WebApi.Infrastructre.Exceptions
{
    public class QuestionNotFoundException(string key, string? message = null) : Exception(message)
    {
        public string Key { get; set; } = key;
    }
}
