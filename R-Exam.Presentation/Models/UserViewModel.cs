namespace R_Exam.Presentation.Models
{
    public class UserViewModel
    {
        public required string Name { get; set; }
        public required string Email { get; set; }
        public string? AvatarPath { get; set; }
    }
}
