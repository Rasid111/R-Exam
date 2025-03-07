using System.ComponentModel.DataAnnotations;

namespace R_Exam.MVC.ViewModels
{
    public class Answer(long id, string title)
    {
        public long Id { get; set; } = id;
        [MaxLength(50)]
        public string Title { get; set; } = title;
        public Answer() : this(0, null)
        {

        }
    }
}
