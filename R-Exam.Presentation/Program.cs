using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Protocols.Configuration;
using R_Exam.Application.Dtos.Question;
using R_Exam.Application.Mappers;
using R_Exam.Application.Services;
using R_Exam.DependencyInjection;
using R_Exam.Domain.Enums;
using R_Exam.Infrastructre.EntityFramework;
using R_Exam.Presentation.Mappers;
using R_Exam.Presentation.Middlewares;

namespace R_Exam.Presentation
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllersWithViews();

            string connectionString = builder.Configuration.GetConnectionString("R-ExamDb") ?? throw new InvalidConfigurationException();
            string accountsConnectionString = builder.Configuration.GetConnectionString("R-ExamAccountsDb") ?? throw new InvalidConfigurationException();

            builder.Services.AddQuestionValidator();
            builder.Services.AddScoped<IQuestionService, QuestionService>();
            builder.Services.AddScoped<ILogService, LogService>();
            builder.Services.AddLogDapperRepository(connectionString);

            builder.Services.AddR_ExamDbContext(connectionString);
            //builder.Services.AddQuestionEntityFrameworkRepository();
            builder.Services.AddQuestionDapperRepository(connectionString);

            builder.Services.AddMediatR(configuration => {
                configuration.RegisterServicesFromAssembly(typeof(QuestionCreateRequestDto).Assembly);
            });

            builder.Services.AddAutoMapper(typeof(Question_QuestionDto_Mapper).Assembly, typeof(QuestionDto_QuestionViewModel_Mapper).Assembly);

            builder.Services.AddAccountDbContext(accountsConnectionString);

            builder.Services.AddR_ExamIdentity();

            builder.Services.AddAuthentication(defaultScheme: CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie();
            builder.Services.AddAuthorization();

            var app = builder.Build();

            var serviceScopeFactory = app.Services.GetRequiredService<IServiceScopeFactory>();
            using (var scope = serviceScopeFactory.CreateScope())
                {
                    var r_ExamDbContext = scope.ServiceProvider.GetService<R_ExamDbContext>();
                    r_ExamDbContext.Database.Migrate();
                    var accountDbContext = scope.ServiceProvider.GetService<AccountDbContext>();
                    accountDbContext.Database.Migrate();

                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
                
                foreach (var roleName in Enum.GetNames(typeof(Roles)))
                {
                    if (!await roleManager.RoleExistsAsync(roleName))
                    {
                        await roleManager.CreateAsync(new IdentityRole(roleName));
                    }
                }

                var email = "admin@admin";
                var result = await userManager.FindByEmailAsync(email);
                if (result == null)
                {
                    var admin = new IdentityUser { UserName = "admin", Email = email };
                    await userManager.CreateAsync(admin, "Admin123!");
                    await userManager.AddToRoleAsync(admin, nameof(Roles.Admin));
                }
            }
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.UseMiddleware<LoggerMiddleware>();
            app.UseMiddleware<ExceptionHandlerMiddleware>();

            app.Run();
        }
    }
}
