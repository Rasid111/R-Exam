using Microsoft.AspNetCore.Http;
using R_Exam.Domain.Models;
using R_Exam.Application.Services;
using R_Exam.Domain.Repositories;
using System.Text.Json;
using System.Text;

namespace R_Exam.Application.Services
{
    public class LogService(ILogRepository repository) : ILogService
    {
        readonly ILogRepository repository = repository;

        public async Task CreateRequestLog(HttpContext httpContext)
        {
            var request = httpContext.Request;
            request.EnableBuffering();
            var buffer = new byte[Convert.ToInt32(request.ContentLength)];
            await request.Body.ReadAsync(buffer);
            var body = Encoding.UTF8.GetString(buffer);
            request.Body.Position = 0;

            var headers = JsonSerializer.Serialize(httpContext.Request.Headers);

            var log = new RequestLog()
            {
                RequestId = httpContext.TraceIdentifier,
                Url = httpContext.Request.Path,
                RequestBody = body,
                RequestHeaders = headers,
                MethodType = httpContext.Request.Method,
                ClientIp = httpContext.Connection.RemoteIpAddress?.ToString(),
                CreationDateTime = DateTime.Now,
            };
            await repository.CreateRequestLog(log);
        }
        public async Task CreateResponseLog(HttpContext httpContext)
        {
            var response = httpContext.Response;
            // TODO - read body
            var headers = JsonSerializer.Serialize(httpContext.Response.Headers);

            var log = new ResponseLog()
            {
                ResponseBody = "",
                ResponseHeaders = headers,
                StatusCode = response.StatusCode,
                EndDateTime = DateTime.Now,
            };
            await repository.CreateResponseLog(log);
        }
    }
}
