using R_Exam.WebApi.Presentation.Responses.Base;

namespace R_Exam.WebApi.Presentation.Responses
{
    public class BadRequestResponse(string? message = null, string? paramName = null) : AbstractResponse(message)
    {
        public string? ParamName { get; set; } = paramName;
    }
}
