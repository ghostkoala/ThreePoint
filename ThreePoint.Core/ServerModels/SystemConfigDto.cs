using System;
using System.ComponentModel.DataAnnotations;

namespace ThreePoint.Core.ServerModels
{
    public class SystemConfigDto
    {
        /// <summary>
        /// 系统名称
        /// </summary>
        [Display(Name = "系统名称")]
        public string SystemName { get; set; }
    }
}