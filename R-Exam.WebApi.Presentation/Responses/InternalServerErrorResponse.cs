using R_Exam.WebApi.Presentation.Responses.Base;

namespace R_Exam.WebApi.Presentation.Responses
{
    public class InternalServerErrorResponse(string? message = null) : AbstractResponse(message) { }
}
