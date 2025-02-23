namespace Models
{
    public class Answer
    {
        public Answer(long id, string title, int questionId)
        {
            Id = id;
            Title = title;
            QuestionId = questionId;
        }
        public long Id { get; set; }
        public string Title { get; set; }
        public long QuestionId { get; set; }

        public Answer()
        {

        }
    }
}
