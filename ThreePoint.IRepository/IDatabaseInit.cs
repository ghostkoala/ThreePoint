using System.Collections.Generic;
using System.Threading.Tasks;
using ThreePoint.Core.ServerModels;

namespace ThreePoint.IRepository
{
    /// <summary>
    /// 数据库初始化契约
    /// </summary>
    public interface IDatabaseInit
    {
        /// <summary>
        /// 初始化数据库数据
        /// </summary>
        /// <param name="menues">菜单列表</param>
        Task<bool> InitAsync(List<MenuDto> menues);

        /// <summary>
        /// 初始化路径码
        /// </summary>
        Task<bool> InitPathCodeAsync();
    }
}