using System.ComponentModel.DataAnnotations;

namespace ThreePoint.E_commerce.Models
{
    /// <summary>
    /// 产品尺寸视图
    /// </summary>
    public class ProductSizeModel
    {
        /// <summary>
        /// 长度
        /// </summary>
        /// <value></value>
        [Display(Name = "长度")]
        public float Long { get; set; }

        /// <summary>
        /// 宽度
        /// </summary>
        /// <value></value>
        [Display(Name = "宽度")]
        public float Width { get; set; }

        /// <summary>
        /// 高度
        /// </summary>
        /// <value></value>
        [Display(Name = "高度")]
        public float Hight { get; set; }

        /// <summary>
        /// 重量
        /// </summary>
        /// <value></value>
        [Display(Name = "重量")]
        public float Weight { get; set; }
    }
}