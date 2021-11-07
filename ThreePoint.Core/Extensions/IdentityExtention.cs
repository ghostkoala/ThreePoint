using System.Security.Claims;
using System.Security.Principal;

namespace ThreePoint.Core.Extensions
{
    /// <summary>
    /// IIdentity扩展
    /// </summary>
    public static class IdentityExtention
    {
        /// <summary>
        /// 获取登录的用户ID
        /// </summary>
        /// <param name="identity">IIdentity</param>
        /// <returns></returns>
        public static string GetLoginUserId(this IIdentity identity)
        {
            var claim = (identity as ClaimsIdentity)?.FindFirst("LoginUserId");
            if (claim != null)
            {
                return claim.Value;
            }
            return string.Empty;
        }
    }
}