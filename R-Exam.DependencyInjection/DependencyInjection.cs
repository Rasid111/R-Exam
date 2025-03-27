using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using R_Exam.Application.Validators;
using R_Exam.Domain.Models;
using R_Exam.Domain.Repositories;
using R_Exam.Infrastructre.EntityFramework;
using R_Exam.Infrastructre.Repositories;

namespace R_Exam.DependencyInjection
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddLogDapperRepository(this IServiceCollection services, string connectionString)
        {
            services.AddScoped<ILogRepository>((serviceProvider) => new LogDapperRepository(connectionString));
            return services;
        }
        public static IServiceCollection AddQuestionDapperRepository(this IServiceCollection services, string connectionString)
        {
            services.AddScoped<IQuestionRepository>((serviceProvider) => new QuestionDapperRepository(connectionString));
            return services;
        }
        public static IServiceCollection AddQuestionEntityFrameworkRepository(this IServiceCollection services)
        {
            services.AddScoped<IQuestionRepository, QuestionEntityFrameWorkRepository>();
            return services;
        }
        public static IServiceCollection AddR_ExamDbContext(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<R_ExamDbContext>(options =>
                options.UseSqlServer(connectionString));
            return services;
        }
        public static IServiceCollection AddQuestionValidator(this IServiceCollection services)
        {
            services.AddScoped<IValidator<Question>, QuestionValidator>();
            return services;
        }
        public static IServiceCollection AddAccountDbContext(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<AccountDbContext>(options =>
                options.UseSqlServer(connectionString));
            return services;
        }
        public static IServiceCollection AddR_ExamIdentity(this IServiceCollection services)
        {
            services.AddIdentity<IdentityUser, IdentityRole>(options =>
            {
                options.User.RequireUniqueEmail = true;
                // Для регистрации админа
                //options.Password.RequiredLength = 0;
                //options.Password.RequiredUniqueChars = 0;
                //options.Password.RequireLowercase = false;
                //options.Password.RequireUppercase = false;
                //options.Password.RequireDigit = false;
                //options.Password.RequireNonAlphanumeric = false;
            })
                .AddEntityFrameworkStores<AccountDbContext>();
            return services;
        }
    }
}
