using System;
using Logicore.Core.Enities;

namespace Logicore.Core.ViewModel
{
    public class ServerExceptionTableViewModel
    {
        /// <summary>
        /// id
        /// </summary>
        public string Id { get; set; }

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
        /// 错误信息
        /// </summary>
        public string ErrMessage { get; set; }

        /// <summary>
        /// 错误类别
        /// </summary>
        public ErrCategory errCategory { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
    }
}