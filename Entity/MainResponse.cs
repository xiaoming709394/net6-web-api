namespace net6_web_api.Entity
{
    public class MainResponse
    {
        public bool IsSuccess { get; set; }
        public string ErrorMessage { get; set; }
        public object Content { get; set; }
    }
}
