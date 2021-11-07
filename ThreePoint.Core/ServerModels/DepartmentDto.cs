using System.ComponentModel.DataAnnotations;
using ThreePoint.Core.SystemConfigurationData;

namespace ThreePoint.Core.ServerModels
{
    /// <summary>
    /// 部门数据
    /// </summary>
    public class DepartmentDto
    {
        /// <summary>
        /// Id
        /// </summary>
        [Display(Name = "部门Id")]
        public string Id { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        [Display(Name = "部门名称")]
        [Required(ErrorMessage = ModelStateValidMessage.Required)]
        public string Name { get; set; }

        /// <summary>
        /// 上级部门
        /// </summary>
        [Display(Name = "上级部门Id")]
        [Required(ErrorMessage = ModelStateValidMessage.Required)]
        public string ParentId { get; set; }

        /// <summary>
        /// 上级部门
        /// </summary>
        [Display(Name = "是否启用")]
        public bool Enabled { get; set; }
    }
}