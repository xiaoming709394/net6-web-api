namespace net6_web_api.Entity
{
    public class LuckySheet
    {
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 内容
        /// </summary>
        public string Data { get; set; }
        /// <summary>
        /// 用户列表
        /// </summary>
        public string User { get; set; }
    }
}
