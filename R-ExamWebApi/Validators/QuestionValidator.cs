using FluentValidation;
using Models;
using System.Net;

namespace R_Exam.Validators
{
    public class QuestionValidator : AbstractValidator<Question>
    {
        public QuestionValidator()
        {
            base.RuleFor((question) => question)
                .Must(question => question.Answers.Any((answer) => answer.Title == question.CorrectAnswerTitle))
                .WithName(nameof(Question.Answers))
                .WithMessage("Question must have correct answer");
            base.RuleFor(question => question.Title)
                .MaximumLength(50)
                .WithName(nameof(Question.Title))
                .WithMessage("{PropertyName} must be less than 50 characters");
            base.RuleFor(question => question.CorrectAnswerTitle)
                .MaximumLength(50)
                .WithName(nameof(Question.CorrectAnswerTitle))
                .WithMessage("{PropertyName} must be less than 50 characters");
            base.RuleForEach(question => question.Answers)
                .ChildRules(answer =>
                {
                    answer.RuleFor(answer => answer.Title)
                    .MaximumLength(50)
                    .WithName(nameof(Answer.Title))
                    .WithMessage("{PropertyName} must be less than 50 characters");
                });
        }
    }
}
