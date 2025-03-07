using Microsoft.AspNetCore.Http;
using R_Exam.Domain.Models;
using R_Exam.Application.Services;
using R_Exam.Domain.Repositories;
using System.Text.Json;

namespace R_Exam.Infrastructre.Services
{
    public class LogService(ILogRepository repository) : ILogService
    {
        readonly ILogRepository repository = repository;

        public async void CreateRequestLog(HttpContext httpContext)
        {
            //var request = httpContext.Request;
            //request.EnableBuffering();
            //var buffer = new byte[Convert.ToInt32(request.ContentLength)];
            //await request.Body.ReadAsync(buffer);
            //var body = Encoding.UTF8.GetString(buffer);
            //request.Body.Position = 0;

            //?

            var headers = JsonSerializer.Serialize(httpContext.Request.Headers);

            var log = new RequestLog()
            {
                RequestId = httpContext.TraceIdentifier,
                Url = httpContext.Request.Path,
                RequestBody = "",
                RequestHeaders = headers,
                MethodType = httpContext.Request.Method,
                ClientIp = httpContext.Connection.RemoteIpAddress.ToString(),
                CreationDateTime = DateTime.Now,
            };
            repository.CreateRequestLog(log);
        }
        public async void CreateResponseLog(HttpContext httpContext)
        {
            var response = httpContext.Response;
            //Считать request.Body я смог, а для reponse нормального способа не нашел
            //using var reader = new StreamReader(response.Body);
            //var body = await reader.ReadToEndAsync();
            var headers = JsonSerializer.Serialize(httpContext.Response.Headers);

            var log = new ResponseLog()
            {
                ResponseBody = "",
                ResponseHeaders = headers,
                StatusCode = response.StatusCode,
                EndDateTime = DateTime.Now,
            };
            repository.CreateResponseLog(log);
        }
    }
}
