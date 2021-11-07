using System.Collections.Generic;
using System.Linq;
using ThreePoint.IRepository;

namespace ThreePoint.Repository
{
    /// <summary>
    /// 路径码服务
    /// </summary>
    public class PathCodeRepository : IPathCodeRepository
    {
        /// <summary>
        /// 获取路径码
        /// </summary>
        /// <returns></returns>
        public IList<string> GetPathCodes()
        {
            //生成路径码
            var codes = new List<string>(26);
            for (var i = 65; i <= 90; i++)
            {
                codes.Add(((char)i).ToString());
            }
            return (from a in codes
                    from b in codes
                    select a + b).OrderBy(item => item).ToList();
        }
    }
}