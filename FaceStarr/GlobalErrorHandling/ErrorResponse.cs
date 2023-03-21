namespace FaceStarr.GlobalErrorHandling
{
    public class ErrorResponse
    {
        public ErrorResponse(int statusCode,string message = null)
        {   
            StatusCode = statusCode;
            Message = message ?? ErrroMessageResponse(statusCode);
        }

        public int StatusCode { get; set; }
        public string  Message { get; set; }
        private string ErrroMessageResponse(int statusCode)
        {
           return statusCode switch 
           {
               400 => "You made a bad request!",
               401 => "You are not authorized!",
               404 => "What you are looking for does not exist!",
               500 => "The Errors that are made are from our end!",
               _=> null
           };
        }
    }
}