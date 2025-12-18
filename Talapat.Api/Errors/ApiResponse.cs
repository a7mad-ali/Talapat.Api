using System.Diagnostics;

namespace Talapat.Api.Errors
{
    public class ApiResponse
    {
        public int StatusCode { get; set; }
        public string?  Message { get; set; }
        public  ApiResponse(int statusCode , String? message )
        {
            StatusCode = statusCode;
            Message = message?? GetMessageFromStatusCode(StatusCode);

        }

        private string? GetMessageFromStatusCode(int StatusCode)
        {
            return StatusCode switch

            {
                400 => "A bad request ,you have made",
                401 => "You are not authorized",
                404 => "Resource was not found",
                500 => "Error u have made were gone to the hell",
                _ => null



            };
                


            
        }
    }
}
