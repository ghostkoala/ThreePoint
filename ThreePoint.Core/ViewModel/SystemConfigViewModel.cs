using System;
using System.ComponentModel.DataAnnotations;

namespace ThreePoint.Core.ViewModel
{
    public class SystemConfigViewModel
    {
        /// <summary>
        /// 系统名称
        /// </summary>
        [Display(Name = "系统名称")]
        public string SystemName { get; set; }

        /// <summary>
        /// 数据初始化是否完成
        /// </summary>
        [Display(Name = "系统是否已初始化")]
        public bool IsDataInited { get; set; }

        /// <summary>
        /// 数据初始化时间
        /// </summary>
        [Display(Name = "系统名称")]
        public string DataInitedDate { get; set; }
    }
}