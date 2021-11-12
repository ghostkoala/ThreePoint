using System.ComponentModel.DataAnnotations;

namespace ThreePoint.E_commerce.Models
{
    /// <summary>
    /// 产口视图
    /// </summary>
    public class ProductModel : BaseModel
    {
        /// <summary>
        /// 名称
        /// </summary>
        /// <value></value>
        [Display(Name = "名称")]
        public string Name { get; set; }

        /// <summary>
        /// 件
        /// </summary>
        /// <value></value>
        [Display(Name = "件数")]
        public int Pcs { get; set; }

        /// <summary>
        /// 采购价
        /// </summary>
        /// <value></value>
        [Display(Name = "采购价")]
        public decimal PurchasePrice { get; set; }

        /// <summary>
        /// 销售价
        /// </summary>
        /// <value></value>
        [Display(Name = "销售价")]
        public decimal SellingPrice { get; set; }

        /// <summary>
        /// 销售币种
        /// </summary>
        /// <value></value>
        [Display(Name = "货币")]
        public string SellingCurrency { get; set; }

        /// <summary>
        /// FBA费用
        /// </summary>
        /// <value></value>
        [Display(Name = "FBA费用")]
        public decimal FBAshipping { get; set; }

        /// <summary>
        /// 货运价格
        /// </summary>
        /// <value></value>
        [Display(Name = "货运价格")]
        public decimal FreightCost { get; set; }

        /// <summary>
        /// 销售佣金
        /// </summary>
        /// <value></value>
        [Display(Name = "销售佣金")]
        public decimal SalesCommission { get; set; }

        /// <summary>
        /// 利润
        /// </summary>
        /// <value></value>
        [Display(Name = "利润")]
        public double profit { get; set; }

        /// <summary>
        /// 利润率
        /// </summary>
        /// <value></value>
        [Display(Name = "利润率")]
        public float ProfitMargin { get; set; }

        /// <summary>
        /// 汇率
        /// </summary>
        /// <value></value>
        [Display(Name = "汇率")]
        public float ExchangeRate { get; set; }

        /// <summary>
        /// 销售地
        /// </summary>
        /// <value></value>
        [Display(Name = "销售地")]
        public string SalesRegions { get; set; }

        /// <summary>
        /// 退货和广告率
        /// </summary>
        /// <value></value>
        [Display(Name = "退货和广告率")]
        public float ReturnAdvertisementPoint { get; set; } = 0.3f;

        /// <summary>
        /// 产品尺寸
        /// </summary>
        /// <value></value>
        public ProductSizeModel Size { get; set; }

        /// <summary>
        /// 采购信息
        /// </summary>
        /// <value></value>
        [Display(Name = "描述")]
        public string Description { get; set; }
    }

}