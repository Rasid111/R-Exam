using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using R_Exam.Application.Exceptions;
using R_Exam.WebApi.Presentation.Responses;
using System.Data;
using System.Net;
using System.Net.Http;

namespace R_Exam.Presentation.Middlewares
{
    public class ExceptionHandlerMiddleware(RequestDelegate next)
    {
        private readonly RequestDelegate next = next;
        private static void HandleException(HttpContext httpContext, string? message = null)
        {
            var tempData = httpContext.RequestServices.GetRequiredService<ITempDataProvider>();
            var tempDataDict = new TempDataDictionary(httpContext, tempData)
            {
                ["ErrorMessage"] = message ?? "Internal Server Error"
            };
            tempDataDict.Save();
            httpContext.Response.Redirect(httpContext.Request.Path);
        }
        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await next.Invoke(httpContext);
            }
            catch (DuplicateNameException ex)
            {
                HandleException(httpContext, ex.Message);
            }
            catch (QuestionNotFoundException ex)
            {
                HandleException(httpContext, ex.Message);
            }
            catch (ArgumentException ex)
            {
                HandleException(httpContext, ex.Message);
            }
            catch (Exception)
            {
                HandleException(httpContext);
            }
        }
    }
}
