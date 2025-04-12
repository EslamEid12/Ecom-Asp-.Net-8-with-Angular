namespace Ecom.Api.Hellper
{
    public class ApiExceptions : ResponsingApi
    {
        public ApiExceptions(int statsCode, string? message = null,string details = null) : base(statsCode, message)
        {
            this.details = details;
        }
        public string details { get; set; }
    }
}
