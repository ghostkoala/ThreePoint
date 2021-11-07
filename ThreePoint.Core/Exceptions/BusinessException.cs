using System;

namespace ThreePoint.Core.Exceptions
{
    /// <summary>
    /// 业务提示信息异常
    /// </summary>
    public class BusinessException : Exception
    {
        public int Code { get; private set; }
        /// <summary>
        /// 定义通用异常
        /// </summary>
        public BusinessException(string message, int code = 500)
        : base(message)
        {
            Code = code;
        }

        public BusinessException(string message, Exception ex, int code = 500)
        : base(message, ex)
        {
            Code = code;
        }
    }
}