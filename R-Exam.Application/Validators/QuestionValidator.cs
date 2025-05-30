﻿using FluentValidation;
using R_Exam.Domain.Models;

namespace R_Exam.Application.Validators
{
    public class QuestionValidator : AbstractValidator<Question>
    {
        public QuestionValidator()
        {
            RuleFor(question => question.Title)
                .MaximumLength(50)
                .WithName(nameof(Question.Title))
                .WithMessage("{PropertyName} must be less than 50 characters");

            RuleFor(question => question.Title)
                .NotNull()
                .NotEmpty()
                .WithName(nameof(Question.Title))
                .WithMessage("{PropertyName} must not be empty");

            RuleFor(question => question.CorrectAnswerTitle)
                .MaximumLength(50)
                .WithName(nameof(Question.CorrectAnswerTitle))
                .WithMessage("{PropertyName} must be less than 50 characters");

            RuleFor(question => question.CorrectAnswerTitle)
                .NotNull()
                .NotEmpty()
                .WithName(nameof(Question.CorrectAnswerTitle))
                .WithMessage("{PropertyName} must not be empty");

            RuleForEach(question => question.Answers)
                .ChildRules(answer =>
                {
                    answer.RuleFor(answer => answer.Title)
                    .MaximumLength(50)
                    .WithName(nameof(Question.Answers))
                    .WithMessage("{PropertyName} must be less than 50 characters");
                });
            RuleFor(question => question.Answers)
                .NotNull()
                .NotEmpty()
                .WithName(nameof(Question.Answers))
                .WithMessage("{PropertyName} must not be empty");

            RuleFor((question) => question)
                .Must(question => question.Answers.Any((answer) => answer.Title == question.CorrectAnswerTitle))
                .WithName(nameof(Question.Answers))
                .WithMessage("Question must have correct answer");
        }
    }
}
