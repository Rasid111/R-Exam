using R_Exam.WebApi.Presentation.Responses.Base;

namespace R_Exam.WebApi.Presentation.Responses
{
    public class NotFoundResponse(string key, string? message = null) : AbstractResponse(message)
    {
        public string Key { get; set; } = key;
    }
}
