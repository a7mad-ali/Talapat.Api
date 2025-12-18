namespace Talapat.Api.Errors
{
    public class ApiExceptionResponse : ApiResponse
    {
        public string? Description { get; set; }
        public ApiExceptionResponse(int statusCode , string? message =null , string? details =null) :base(statusCode,message)
        {

        }
    }
}
