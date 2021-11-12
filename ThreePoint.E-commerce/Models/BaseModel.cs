namespace ThreePoint.E_commerce.Models
{
    /// <summary>
    /// 基础视图
    /// </summary>
    public class BaseModel
    {
        /// <summary>
        /// Id
        /// </summary>
        /// <value></value>
        [Display(Name = "Id")]
        public string Id { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        /// <value></value>
        [Display(Name = "创建时间")]
        public string CreateTime { get; set; }

        /// <summary>
        /// 移除时间
        /// </summary>
        /// <value></value>
        [Display(Name = "移除时间")]
        public string ReMoveTime { get; set; }
    }
}