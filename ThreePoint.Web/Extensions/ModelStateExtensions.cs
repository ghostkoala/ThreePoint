using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace ThreePoint.Web.Extensions
{
    /// <summary>
    /// 模型验证扩展
    /// </summary>
    public static class ModelStateExtensions
    {
        /// <summary>
        /// 获得所有错误
        /// </summary>
        /// <param name="controller">当前控制器</param>
        /// <returns></returns>
        public static List<string> ExpendErrors(this Controller controller)
        {
            List<string> err = new List<string>();
            foreach (var item in controller.ModelState.Values)
            {
                if (item.Errors.Count > 0)
                {
                    for (int i = item.Errors.Count - 1; i >= 0; i--)
                    {
                        err.Add(item.Errors[i].ErrorMessage);
                    }
                }
            }
            return err;
        }
    }
}

