namespace Talabat.APIs.Errors
{
    public class ApiResponse
    {
        public int StatusCode { get; set; }

        public string? Message { get; set; }

        public ApiResponse(int statusCode, string? message=null)
        {
            StatusCode= statusCode;
            Message = message ?? GetDefaultErrorMessageByStatus(statusCode);
        }

        private string GetDefaultErrorMessageByStatus(int statusCode) {

            return statusCode switch
            {
                404 => "Resourse was not found!",
                400 => "A bad request, you have made!",
                401 => "You are not Authorized!",
                500 => "Errors are the path to the dark side . Errors leads to anger",
                _ => null
            } ;
        }

    }
}
