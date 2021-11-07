using System.Text.Json;

namespace ThreePoint.Web.Extensions
{
    /// <summary>
    /// 返回对象全小写
    /// </summary>
    public class LowercasePolicy : JsonNamingPolicy
    {
        public override string ConvertName(string name) =>
            name.ToLower();
    }

}