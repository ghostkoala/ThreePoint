using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using ThreePoint.Core.Enities;
using ThreePoint.Core.SystemConfigurationData;

namespace ThreePoint.Core.ServerModels
{
    /// <summary>
    /// 角色数据
    /// </summary>
    public class RoleDto
    {
        /// <summary>
        /// Id
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 角色名称
        /// </summary>
        [Display(Name = "角色名称")]
        [Required(ErrorMessage = ModelStateValidMessage.Required)]
        [MinLength(2, ErrorMessage = ModelStateValidMessage.MinLength)]
        [MaxLength(20, ErrorMessage = ModelStateValidMessage.MaxLength)]
        public string Name { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        [Display(Name = "角色描述")]
        [Required(ErrorMessage = ModelStateValidMessage.Required)]
        [MinLength(2, ErrorMessage = ModelStateValidMessage.MinLength)]
        [MaxLength(50, ErrorMessage = ModelStateValidMessage.MaxLength)]
        public string Description { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        [Display(Name = "是否启用")]
        [Required(ErrorMessage = ModelStateValidMessage.Required)]
        public bool Enabled { get; set; } = true;

        public static implicit operator RoleDto(RoleEntity v)
        {
            RoleDto dto = new RoleDto()
            {
                Id = v.Id,
                Name = v.Name,
                Description = v.Description,
                Enabled = v.Enabled
            };
            return dto;
        }
    }
}