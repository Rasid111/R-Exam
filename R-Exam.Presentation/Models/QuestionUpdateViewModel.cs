﻿using System.ComponentModel.DataAnnotations;

namespace R_Exam.Presentation.Models
{
    public class QuestionUpdateViewModel
    {
        [Range(1, int.MaxValue)]
        public int Id { get; set; }
        [MaxLength(50)]
        public required string Title { get; set; }
        public required string Answers { get; set; }
        [MaxLength(50)]
        public required string CorrectAnswerTitle { get; set; }
    }
}
