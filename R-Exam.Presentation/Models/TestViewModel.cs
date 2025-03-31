namespace R_Exam.Presentation.Models
{
    public class TestViewModel(List<QuestionDetailsViewModel> questions)
    {
        public TestViewModel() : this([]) { } 
        public List<QuestionDetailsViewModel> Questions { get; set; } = questions;
        public int Number { get; set; }
        public string? SelectedAnswer { get; set; } = null;
        public int QuestionsCount { get; set; }
    }
}
