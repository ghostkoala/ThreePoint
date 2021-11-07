namespace ThreePoint.Core.ViewModel
{
    /// <summary>
    /// Ztree视图
    /// </summary>
    public class ZtreeViewModel
    {
        /// <summary>
        /// Id
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 父级Id
        /// </summary>
        public string PId { get; set; }
        /// <summary>
        /// 名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 是否选中
        /// </summary>
        public bool Checked { get; set; }
    }
}