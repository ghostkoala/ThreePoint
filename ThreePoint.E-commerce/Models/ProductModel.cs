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
        [Display(Name = "件")]
        public int pcs { get; set; }

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
        public float ReturnAdvertisementPoint { get; set; }
    }

}