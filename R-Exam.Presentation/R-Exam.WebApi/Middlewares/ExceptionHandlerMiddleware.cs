using R_Exam.Application.Exceptions;
using R_Exam.WebApi.Presentation.Responses;
using System.Net;

namespace R_Exam.Presentation.Middlewares
{
    public class ExceptionHandlerMiddleware(RequestDelegate next)
    {
        private readonly RequestDelegate next = next;

        public async Task InvokeAsync(HttpContext httpContext)
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
                await httpContext.Response.WriteAsJsonAsync(new InternalServerErrorResponse(ex.Message ?? "Unhandled Internal Server Error"));
            }
        }
    }
}
