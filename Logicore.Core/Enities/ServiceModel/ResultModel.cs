namespace Logicore.Core.Enities.ServiceModel
{
    /// <summary>
    /// Json返回值模型
    /// </summary>
    /// <typeparam name="T">返回对象</typeparam>
    public class ResultModel<T>
    {
        /// <summary>
        /// 状态
        /// </summary>
        public bool Status { get; set; } = false;

        /// <summary>
        /// 状态码
        /// </summary>
        public int Code { get; set; } = 0;

        /// <summary>
        /// 消息
        /// </summary>
        public string Message { get; set; } = null;

        /// <summary>
        /// 返回数据
        /// </summary>
        public T Data { get; set; }

        /// <summary>
        /// ctor
        /// </summary>
        public ResultModel()
        {

        }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="code">状态码</param>
        /// <param name="message">消息</param>
        public ResultModel(int code, string message)
        {
            this.Code = code;
            this.Message = message;
        }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="code">状态码</param>
        /// <param name="message">消息</param>
        /// <param name="status">页面状态</param>
        public ResultModel(int code, string message, bool status)
        {
            this.Status = status;
            this.Code = code;
            this.Message = message;
        }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="code">状态码</param>
        /// <param name="message">消息</param>
        /// <param name="status">页面状态</param>
        /// <param name="data">返回数据</param>
        public ResultModel(int code, bool status, string message, T data)
        {
            this.Status = status;
            this.Code = code;
            this.Message = message;
            this.Data = data;
        }
    }
}