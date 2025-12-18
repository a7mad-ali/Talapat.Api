namespace Talapat.Api.Errors
{
    public class ApiValidationErrorResponse : ApiResponse
    {
        public IEnumerable<string> ValidationErrors { get; set; }

        public ApiValidationErrorResponse() : base(400, null)
        {
            ValidationErrors = new List<string>();
        }
    }
}
