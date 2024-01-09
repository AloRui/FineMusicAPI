namespace FineMusicAPI.Entities
{
    public class RequestResultInfo
    {
        public int Code { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
        public DateTime RequestTime { get; } = DateTime.Now;
        public DateTime RequestUTCTime { get; } = DateTime.UtcNow;

        public static RequestResultInfo Success(object data, string message = "Success")
        {
            return new RequestResultInfo
            {
                Code = 1,
                Data = data,
                Message = message,
            };
        }

        public static RequestResultInfo Failed(string message = "Failed")
        {
            return new RequestResultInfo
            {
                Code = -1,
                Message = message,
            };
        }
    }
}