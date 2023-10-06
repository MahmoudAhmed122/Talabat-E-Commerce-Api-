namespace Talabat.APIs.Errors
{
    public class ApiValidationErrorResponse:ApiResponse
    {

        public ICollection<string> Errors { get; set; }

        public ApiValidationErrorResponse() : base(400)
        {
            Errors = new List<string>();    
        }
    }
}


// Validation Error is atype of BadRequest