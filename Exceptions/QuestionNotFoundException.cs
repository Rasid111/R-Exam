namespace R_Exam.Exceptions
{
    public class QuestionNotFoundException : Exception
    {
        public QuestionNotFoundException() { }

        public QuestionNotFoundException(string message) : base(message) { }
    }
}
