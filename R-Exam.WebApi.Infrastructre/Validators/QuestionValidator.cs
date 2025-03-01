using FluentValidation;
using R_Exam.WebApi.Core.Models;

namespace R_Exam.WebApi.Infrastructre.Validators
{
    public class QuestionValidator : AbstractValidator<Question>
    {
        public QuestionValidator()
        {
            RuleFor((question) => question)
                .Must(question => question.Answers.Any((answer) => answer.Title == question.CorrectAnswerTitle))
                .WithName(nameof(Question.Answers))
                .WithMessage("Question must have correct answer");
            RuleFor(question => question.Title)
                .MaximumLength(50)
                .WithName(nameof(Question.Title))
                .WithMessage("{PropertyName} must be less than 50 characters");
            RuleFor(question => question.CorrectAnswerTitle)
                .MaximumLength(50)
                .WithName(nameof(Question.CorrectAnswerTitle))
                .WithMessage("{PropertyName} must be less than 50 characters");
            RuleForEach(question => question.Answers)
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
