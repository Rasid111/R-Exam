using R_Exam.WebApi.Core.Services.Base;
using R_Exam.WebApi.Infrastructre.Exceptions;
using R_Exam.WebApi.Presentation.Responses;
using System.Net;

namespace R_Exam.WebApi.Presentation.Middlewares
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate next;
        public ExceptionHandlerMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext, ILogService logService)
        {
            try
            {
                await next.Invoke(httpContext);
            }
            catch (QuestionNotFoundException ex)
            {
                httpContext.Response.StatusCode = (int)HttpStatusCode.NotFound;
                await httpContext.Response.WriteAsJsonAsync(new NotFoundResponse(ex.Key, ex.Message));
            }
            catch (ArgumentException ex)
            {
                httpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                await httpContext.Response.WriteAsJsonAsync(new BadRequestResponse(ex.Message, ex.ParamName));
            }
            catch (Exception ex)
            {
                httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                // Где лучше дефолтное значение для message указать? Тут или в классе самого респонса?
                await httpContext.Response.WriteAsJsonAsync(new InternalServerErrorResponse(ex.Message ?? "Unhandled Internal Server Error"));
            }
        }
    }
}
