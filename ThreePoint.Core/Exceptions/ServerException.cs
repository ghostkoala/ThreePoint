using System;

namespace ThreePoint.Core.Exceptions
{
    /// <summary>
    /// 系统提示信息异常
    /// </summary>
    public class ServerException : Exception
    {
        public int Code { get; private set; }
        /// <summary>
        /// 定义通用异常
        /// </summary>
        public ServerException(string message, int code = 500)
        : base(message)
        {
            Code = code;
        }

        public ServerException(string message, Exception ex, int code = 500)
        : base(message, ex)
        {
            Code = code;
        }
    }
}