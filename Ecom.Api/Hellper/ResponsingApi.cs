namespace Ecom.Api.Hellper
{
    public class ResponsingApi
    {
        public ResponsingApi(int statsCode, string? message=null)
        {
            StatsCode = statsCode;
            Message = message?? GetMessageFromStatsCode(statsCode);
        }
        private string GetMessageFromStatsCode(int statsCode)
        {
            return statsCode switch
            {
                200 => "Done",
                400 => "Bad Request",
                401 => "Un Authrized",
                500 => "Server Error ",
                _ => null,
            };
        }
        public int StatsCode { get; set; }
        public string? Message { get; set; }
    }
}
