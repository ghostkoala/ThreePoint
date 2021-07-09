using System.Collections.Generic;

namespace Logicore.IRepository
{
    /// <summary>
    /// 路径码仓储接口
    /// </summary>
    public interface IPathCodeRepository
    {
        /// <summary>
        /// 获取路径码
        /// </summary>
        /// <returns></returns>
        IList<string> GetPathCodes();
    }
}