namespace net6_web_api.Entity
{
    /// <summary>
    /// 接口返回实体
    /// </summary>
    public class ResponseResult
    {
        public int code { get; set; }

        public object data { get; set; }

        public string message { get; set; }
    }
}
