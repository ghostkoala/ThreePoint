using System;

namespace ThreePoint.Core.Enities
{
    /// <summary>
    /// 系统错误实体
    /// </summary>
    public class ServerExceptionEntity
    {
        /// <summary>
        /// id
        /// </summary>
        public string Id { get; set; } = Guid.NewGuid().ToString("N");

        /// <summary>
        /// 操作人Id
        /// </summary>
        public string useId { get; set; }

        /// <summary>
        /// 错误代码
        /// </summary>
        public int Code { get; set; }

        /// <summary>
        /// 执行操作的连接
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// 执行的方法
        /// </summary>
        public string Method { get; set; }

        /// <summary>
        /// 执行操作
        /// </summary>
        public string ActionArguments { get; set; }

        /// <summary>
        /// 错误信息
        /// </summary>
        public string ErrMessage { get; set; }

        /// <summary>
        /// 跟踪信息
        /// </summary>
        public string StackTrace { get; set; }

        /// <summary>
        /// 错误类别
        /// </summary>
        public ErrCategory errCategory { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; } = DateTime.Now;
    }

    public enum ErrCategory
    {
        BusinessException,
        ServerException,
        Others
    }
}